namespace Order_System.Models.Accessory
{
    public class IT_accessories
    {
        public long AccessoryId { get; set; }
        public string AccessoryName { get; set; } = null!;
        public decimal AccessoryUnitPrice { get; set; }
        public string BrandName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

    }
}
