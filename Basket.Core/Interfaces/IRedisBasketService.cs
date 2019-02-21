using Basket.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface IRedisBasketService
    {
        /// <summary>
        /// Getting exist basket with cacheKey(customerId)
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        Task<CustomerBasketDTO> GetBasketAsync(string cacheKey);

        /// <summary>
        /// Adding product item to basket
        /// </summary>
        /// <param name="basketItemDTO"></param>
        /// <returns></returns>
        Task<ApiResultDTO<CustomerBasketDTO>> AddItemToBasket(BasketItemDTO basketItemDTO);

        Task<CustomerBasketDTO> UpdateBasketAsync(CustomerBasketDTO customerBasket);
        
        Task DeleteBasketAsync(string cacheKey);
    }
}
