using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.DTO
{
    public class BasketItemDTO
    {
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string CustomerId { get; set; }
        public string ProductName { get; set; }
    }
}
