namespace Order_System.Models.Order_detail
{
    public class Order_detail_Model
    {
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public long AccessoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
