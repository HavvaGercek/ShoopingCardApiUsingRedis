using Basket.Core.DTO;

namespace Basket.Core.Interfaces
{
    public interface IBasketService
    {
        /// <summary>
        /// Checking enough product is exist or not 
        /// </summary>
        /// <param name="checkProductStockDTO"></param>
        /// <returns></returns>
        bool CheckProductStock(CheckProductStockDTO checkProductStockDTO);
    }
}
