using Order_System.Models.Order_detail;

namespace Order_System.Models.Order
{
    public class OrderModel
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public DateTime OrderDate { get; set; }    
        public decimal TotalAmount { get; set; }
        public List<Order_detail_Model> OrderDetails { get; set; } = new List<Order_detail_Model>();
    }
}
