﻿namespace NykantMVC.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public long Price { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string ContentId { get; set; }
    }
}
