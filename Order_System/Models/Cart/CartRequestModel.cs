﻿namespace Order_System.Models.Cart
{
    public class CartRequestModel
    {
        public long UserId { get; set; }
        public long AccessoryId { get; set; }
        //public string AccessoryName { get; set; } = null!;
        //public decimal AccessoryUnitPrice { get; set; }
        public int Quantity { get; set; }
        //public decimal TotalPrice { get; set; }
        //public bool IsActive { get; set; }
    }
}
