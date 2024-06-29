
namespace Order_System.Controllers;

public class CartController : BaseController
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
            List<SqlParameter> parameters = new() { new SqlParameter("@IsActive", true) };
            List<CartResponseModel> lst = _adoDotNetService.Query<CartResponseModel>(
                query,
                parameters.ToArray()
            );

            return Content(lst);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpGet]
    [Route("/api/cart/{userID}")]
    public IActionResult GetCartListByUserId(long userID)
    {
        try
        {
            if (userID <= 0)
                return BadRequest("User Id cannot be empty.");

            string query = CartQuery.GetCartListByUserIdQuery();
            List<SqlParameter> parameters =
                new() { new SqlParameter("@UserId", userID), new SqlParameter("@IsActive", true) };
            List<CartResponseModel> lst = _adoDotNetService.Query<CartResponseModel>(
                query,
                parameters.ToArray()
            );

            return Content(lst);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpPost]
    [Route("/api/cart")]
    public async Task<IActionResult> CreateCartAsync([FromBody] CartRequestModel requestModel)
    {
        try
        {
            if (requestModel.AccessoryId == 0)
                return BadRequest();

            if (requestModel.Quantity == 0)
                return BadRequest();

            if (requestModel.UserId == 0)
                return BadRequest();

            string query = CartQuery.CreateCartQuery();
            List<SqlParameter> parameters =
                new()
                {
                    new SqlParameter("@AccessoryId", requestModel.AccessoryId),
                    new SqlParameter("@UserId", requestModel.UserId),
                    new SqlParameter("@Quantity", requestModel.Quantity),
                    new SqlParameter("@AccessoryName", requestModel.AccessoryName),
                    new SqlParameter("@IsActive", true)
                };
            int result = await _adoDotNetService.ExecuteAsync(query, parameters.ToArray());

            return result > 0 ? Created("Cart Created!") : BadRequest("Creating Fail!");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpPut]
    [Route("/api/cart/{id}")]
    public async Task<IActionResult> UpdateCartAsync([FromBody] UpdateCartRequestModel requestModel, long id)
    {
        try
        {
            string query = CartQuery.UpdateCartQuery();
            List<SqlParameter> parameters =
                new()
                {
                    new SqlParameter("@Quantity", requestModel.Quantity),
                    new SqlParameter("@CartId", id)
                };
            int result = await _adoDotNetService.ExecuteAsync(query, parameters.ToArray());

            return result > 0
                ? Accepted("Updating Successful!")
                : BadRequest("Updating Fail!");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpDelete]
    [Route("/api/cart/{id}")]
    public async Task<IActionResult> DeleteCartAsync(long id)
    {
        try
        {
            if (id == 0)
                return BadRequest();

            string query = CartQuery.DeleteCartQuery();
            List<SqlParameter> parameters =
                new() { new SqlParameter("@IsActive", false), new SqlParameter("@CartId", id) };
            int result = await _adoDotNetService.ExecuteAsync(query, parameters.ToArray());

            return result > 0 ? Accepted("Cart Deleted!") : BadRequest("Deleting Fail!");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}
