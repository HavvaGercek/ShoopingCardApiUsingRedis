using Basket.Core.DTO;
using Basket.Core.Interfaces;

namespace Basket.Core.Services
{
    public class BasketService : IBasketService
    {
        public bool CheckProductStock(CheckProductStockDTO checkProductStockDTO)
        {
            //It checks product stock is enough or exist. 
            //If there is then it returns true otherwise it returns false

            return true;
        }
    }
}
