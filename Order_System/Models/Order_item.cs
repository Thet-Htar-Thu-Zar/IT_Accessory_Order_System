namespace Order_System.Models
{
    public class Order_item
    {
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public long AccessoryId { get; set; }
        public decimal AccessoryUnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set;}
        public decimal TotalPrice { get; set; }
    }
}
