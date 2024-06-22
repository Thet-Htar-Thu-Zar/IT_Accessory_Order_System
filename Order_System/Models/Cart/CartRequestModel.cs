namespace Order_System.Models.Cart
{
    public class CartRequestModel
    {
        public long UserId { get; set; }
        public long AccessoryId { get; set; }
        public string AccessoryName { get; set; } = null!;
        public int Quantity { get; set; }
    
    }
}
