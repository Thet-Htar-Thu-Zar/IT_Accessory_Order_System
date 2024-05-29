namespace Order_System.Models
{
    public class IT_accessory
    {
        public long AccessoryId { get; set; }
        public string AccessoryName { get; set; } = null!;
        public decimal AccessoryUnitPrice { get; set;} 
        public string BrandName { get; set;} = null!;
        public int Quantity { get; set;}
        public decimal Discount { get; set;}
        public bool IsActive { get; set; }
    }
}
