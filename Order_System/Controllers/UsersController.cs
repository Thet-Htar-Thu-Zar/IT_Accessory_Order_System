using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_System.Models;
using Order_System.Service;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Order_System.Enums;

namespace Order_System.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AdoDotNetService _adoDotNetService;

        public UsersController(IConfiguration configuration, AdoDotNetService adoDotNetService)
        {
            _configuration = configuration;
            _adoDotNetService = adoDotNetService;
        }

        [HttpPost]
        [Route("/api/account/register")]

        public IActionResult Register([FromBody] RegisterRequestModel requestModel)
        {
            try
            {
                if (string.IsNullOrEmpty(requestModel.FirstName))
                    return BadRequest("FirstName cannot be empty.");

                if (string.IsNullOrEmpty(requestModel.LastName))
                    return BadRequest(" LastName cannot be empty.");

                if (string.IsNullOrEmpty(requestModel.Email))
                    return BadRequest("Email cannot be empty.");

                if (string.IsNullOrEmpty(requestModel.PhoneNo))
                    return BadRequest("PhoneNumber cannot be empty.");

                if (string.IsNullOrEmpty(requestModel.Password))
                    return BadRequest("Password cannot be empty.");

                string duplicateQuery = @"SELECT [UserId]
      ,[FirstName]
      ,[LastName]
      ,[Email]
      ,[PhoneNo]
      ,[Password]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE Email = @Email AND IsActive = @IsActive";
                List<SqlParameter> duplicateParams = new()
                {
                    new SqlParameter("@Email", requestModel.Email),
                    new SqlParameter("@IsActive", true)
                };
                DataTable dt = _adoDotNetService.QueryFirstOrDefault(duplicateQuery, duplicateParams.ToArray());

                if (dt.Rows.Count > 0)
                    return Conflict("User with this email already exists!");

                string query = @"INSERT INTO [dbo].[Users]
           ([FirstName]
           ,[LastName]
           ,[Email]
           ,[PhoneNo]
           ,[Password]
           ,[UserRole]
           ,[IsActive])
VALUES (@FirstName, @LastName, @Email, @PhoneNo, @Password, @UserRole, @IsActive)";
                List<SqlParameter> parameters = new()
                {
                    new SqlParameter("@FirstName", requestModel.FirstName),
                    new SqlParameter("@LastName", requestModel.LastName),
                    new SqlParameter("@Email", requestModel.Email),
                    new SqlParameter("@PhoneNo", requestModel.PhoneNo),
                    new SqlParameter("@Password", requestModel.Password),
                    new SqlParameter("@UserRole", EnumUserRoles.User.ToString()),
                    new SqlParameter("@IsActive", true)
                };
                int result = _adoDotNetService.Execute(query, parameters.ToArray());

                return result > 0 ? StatusCode(201, "Registration Successful!") : BadRequest("Fail!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/account/login")]
        public IActionResult Login([FromBody] LoginRequestModel requestModel)
        {
            try
            {
                if (string.IsNullOrEmpty(requestModel.Email))
                    return BadRequest("Email cannot be empty.");

                if (string.IsNullOrEmpty(requestModel.Password))
                    return BadRequest("Password cannot be empty.");

                string query = @"SELECT [UserId]
      ,[FirstName]
      ,[LastName]
      ,[Email]
      ,[PhoneNo]
      ,[Password]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE Email = @Email AND IsActive = @IsActive AND Password = @Password && UserRole = @UserRole";
                List<SqlParameter> parameters = new()
                {
                    new SqlParameter("@Email", requestModel.Email),
                    new SqlParameter("@Password", requestModel.Password),
                    new SqlParameter("@UserRole", EnumUserRoles.User.ToString()),
                    new SqlParameter("@IsActive", true),
                };
                DataTable user = _adoDotNetService.QueryFirstOrDefault(query, parameters.ToArray());

                if (user.Rows.Count == 0)
                    return NotFound("User Not found.");

                string jsonStr = JsonConvert.SerializeObject(user);
                List<Users> lst = JsonConvert.DeserializeObject<List<Users>>(jsonStr)!;

                return Ok(lst[0]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

