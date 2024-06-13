using Microsoft.AspNetCore.Mvc;
using Order_System.Service;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Order_System.Enums;
using Order_System.Models.User;
using System.Text.RegularExpressions;
using Order_System.Queries;

namespace Order_System.Controllers;

public class UserController : ControllerBase
{
    private readonly AdoDotNetService _adoDotNetService;

    public UserController(AdoDotNetService adoDotNetService)
    {
        _adoDotNetService = adoDotNetService;
    }

    [HttpPost]
    [Route("/api/account/register")]

    public IActionResult Register([FromBody] RegisterRequestModel requestModel)
    {
        try
        {
            #region Validation

            if (string.IsNullOrEmpty(requestModel.FirstName))
                return BadRequest("FirstName cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.LastName))
                return BadRequest(" LastName cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.Email))
                return BadRequest("Email cannot be empty.");          

           

            if (string.IsNullOrEmpty(requestModel.PhoneNo) ||
        !(requestModel.PhoneNo.Length <= 13 && Regex.IsMatch(requestModel.PhoneNo, @"^\+\d{1,12}$")))
            {
                return BadRequest("PhoneNumber cannot be empty and must be 10 digits with an optional leading +.");
            }

            if (string.IsNullOrEmpty(requestModel.Password) ||
                !(requestModel.Password.Length >= 8 && Regex.IsMatch(requestModel.Password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))) //ThetHtar@8
            {
                return BadRequest("Password cannot be empty and must contain at least 8 characters including uppercase letters, lowercase letters, a number, and special characters.");
            }

            #endregion

            #region Email Duplicate Testing

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

            #endregion

            #region Register Case

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

            #endregion

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
            #region Validation

            if (string.IsNullOrEmpty(requestModel.Email))
                return BadRequest("Email cannot be empty.");

            if (string.IsNullOrEmpty(requestModel.Password))
                return BadRequest("Password cannot be empty.");

            #endregion

            string query = @"SELECT [UserId]
      ,[FirstName]
      ,[LastName]
      ,[Email]
      ,[PhoneNo]
      ,[Password]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE Email = @Email AND IsActive = @IsActive AND Password = @Password AND UserRole = @UserRole";
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

            return Ok(JsonConvert.SerializeObject(user));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch]
    [Route("/api/account/{id}")]
    public IActionResult UpdateAccount([FromBody] UpdateUserRequestModel requestModel, long id)
    {
        try
        {
            //if (string.IsNullOrEmpty(requestModel.ExpenseCategoryName))
            //    return BadRequest("Category name cannot be empty.");

            string duplicateQuery = UserQuery.CheckUpdateUserDuplicateQuery();
            List<SqlParameter> duplicateParams = new()
            {
                new SqlParameter("@FirstName", requestModel.FirstName),
                new SqlParameter("@LastName", requestModel.LastName),
                new SqlParameter("@IsActive", true),
                new SqlParameter("@UserId", id)
            };
            DataTable dt = _adoDotNetService.QueryFirstOrDefault(duplicateQuery, duplicateParams.ToArray());
            if (dt.Rows.Count > 0)
                return Conflict("FirstName already exists."); 

            string query = UserQuery.UpdateUserQuery();
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@FirstName", requestModel.FirstName),
                new SqlParameter("@LastName", requestModel.LastName),
                new SqlParameter("@UserId", id)
            };
            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            return result > 0 ? StatusCode(202, "Updating Successful!") : BadRequest("Updating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}