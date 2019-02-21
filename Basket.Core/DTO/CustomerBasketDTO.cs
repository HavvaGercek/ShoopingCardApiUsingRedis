using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Core.DTO
{
    public class CustomerBasketDTO
    {
        public string CustomerId { get; set; }
        public List<BasketItemDTO> BasketItems { get; set; }

        public CustomerBasketDTO()
        {
            BasketItems = new List<BasketItemDTO>();
        }
    }
}
