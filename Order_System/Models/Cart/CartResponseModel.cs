namespace Order_System.Models.Cart;

public class CartResponseModel
{
    public long CartId { get; set; }
    public string AccessoryName { get; set; } = null!;
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
}
