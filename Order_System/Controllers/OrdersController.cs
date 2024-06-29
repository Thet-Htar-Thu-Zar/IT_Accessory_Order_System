namespace Order_System.Controllers;

public class OrdersController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public OrdersController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("/api/orders")]
    public async Task<IActionResult> CreateOrdersWithOrederDetails([FromBody] OrderModel order)
    {
        var connectionString = _configuration.GetConnectionString("DbConnection");

        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        using var transaction = connection.BeginTransaction();

        try
        {
            string query =
                "INSERT INTO [Order] (OrderDate, UserId, TotalAmount) VALUES (@OrderDate, @UserId, @TotalAmount); SELECT SCOPE_IDENTITY();";
            var orderCommand = new SqlCommand(query, connection, transaction);

            orderCommand.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            orderCommand.Parameters.AddWithValue("@UserId", order.UserId);
            orderCommand.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);

            order.OrderId = Convert.ToInt64(await orderCommand.ExecuteScalarAsync());

            foreach (var detail in order.OrderDetails)
            {
                var detailCommand = new SqlCommand(
                    "INSERT INTO Order_detail (OrderId, AccessoryId, UnitPrice, Quantity) VALUES (@OrderId, @AccessoryId, @UnitPrice, @Quantity)",
                    connection,
                    transaction
                );

                detailCommand.Parameters.AddWithValue("@OrderId", order.OrderId);
                detailCommand.Parameters.AddWithValue("@AccessoryId", detail.AccessoryId);
                detailCommand.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);
                detailCommand.Parameters.AddWithValue("@Quantity", detail.Quantity);

                await detailCommand.ExecuteNonQueryAsync();
            }
            await transaction.CommitAsync();
            return Ok(new { Message = "Order Successful!", order.OrderId });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return BadRequest(new { Message = ex.ToString() });
        }
    }
}
