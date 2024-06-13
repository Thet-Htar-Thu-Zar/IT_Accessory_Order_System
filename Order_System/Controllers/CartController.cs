using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_System.Models.Cart;
using Order_System.Queries;
using Order_System.Service;
using System.Data.SqlClient;

namespace Order_System.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService;

        public CartController(AdoDotNetService adoDotNetService)
        {
            _adoDotNetService = adoDotNetService;
        }

        [HttpGet]
        [Route("/api/cart")]
        public IActionResult GetList()
        {
            try
            {
                string query = CartQuery.GetCartListQuery();
                List<SqlParameter> parameters = new()
            {
                new SqlParameter("@IsActive", true)
            };
                List<CartResponseModel> lst = _adoDotNetService.Query<CartResponseModel>(query, parameters.ToArray());

                return Ok(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/cart/{userID}")]
        public IActionResult GetIncomeListByUserId(long userID)
        {
            try
            {
                if (userID <= 0)
                    return BadRequest("User Id cannot be empty.");

                string query = CartQuery.GetIncomeListByUserIdQuery();
                List<SqlParameter> parameters = new()
            {
                new SqlParameter("@UserId", userID),
                new SqlParameter("@IsActive", true)
            };
                List<CartResponseModel> lst = _adoDotNetService.Query<CartResponseModel>(query, parameters.ToArray());

                return Ok(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/cart")]
        public IActionResult CreateOrder([FromBody] CartRequestModel requestModel)
        {
            try
            {
                if (requestModel.AccessoryId == 0 || requestModel.Quantity == 0 || /*requestModel.TotalPrice == 0 ||*/ requestModel.UserId == 0 )
                    return BadRequest();

                string query = CartQuery.CreateCartQuery();
                List<SqlParameter> parameters = new()
            {
                new SqlParameter("@AccessoryId", requestModel.AccessoryId),
                new SqlParameter("@UserId", requestModel.UserId),
                new SqlParameter("@Quantity", requestModel.Quantity),
                //new SqlParameter("@AccessoryName", requestModel.AccessoryName),
                new SqlParameter("@IsActive", true)
            };
                int result = _adoDotNetService.Execute(query, parameters.ToArray());

                return result > 0 ? StatusCode(201, "Cart Created!") : BadRequest("Creating Fail!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //[HttpPatch]
        //[Route("")]
        //public IActionResult UpdateOrder([FromBody] CartRequestModel requestModel)
        //{
        //}
    }
}
