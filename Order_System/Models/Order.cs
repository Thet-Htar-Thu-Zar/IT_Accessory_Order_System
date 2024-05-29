namespace Order_System.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public string OrderNo { get; set; } = null!;
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; } = null!;
    }
}
