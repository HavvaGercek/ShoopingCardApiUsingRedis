using System.Collections.Generic;

namespace Basket.Api.Models
{
    public class CustomerBasketModel
    {
        public string CustomerId { get; set; }
        public List<BasketItemModel> BasketItems { get; set; }
        public CustomerBasketModel()
        {
            BasketItems = new List<BasketItemModel>();
        }
    }
}
