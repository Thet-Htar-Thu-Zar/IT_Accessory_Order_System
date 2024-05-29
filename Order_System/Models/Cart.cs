namespace Order_System.Models
{
    public class Cart
    {
        public long CartId { get; set; }
        public long UserId { get; set;}
        public long AccessoryId { get; set; }
        public decimal AccessoryUnitPrice { get; set;}
        public decimal Discount { get; set;}
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set;}
    }
}
