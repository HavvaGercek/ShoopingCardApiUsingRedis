using Basket.Core.DTO;
using Basket.Core.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Core.Services
{
    public class RedisBasketService : IRedisBasketService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IBasketService _basketService;

        public RedisBasketService(IDistributedCache distributedCache, IBasketService basketService)
        {
            _distributedCache = distributedCache;
            _basketService = basketService;
        }
        
        public async Task<CustomerBasketDTO> GetBasketAsync(string cacheKey)
        {
            var customerBasket = await _distributedCache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(customerBasket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerBasketDTO>(customerBasket);
        }

        public async Task<CustomerBasketDTO> UpdateBasketAsync(CustomerBasketDTO customerBasket)
        {
            //if there is not any action about the card in a day, it is fallen out of cache
            var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));
           
            await _distributedCache.SetStringAsync(customerBasket.CustomerId, JsonConvert.SerializeObject(customerBasket), option);
            
            return await GetBasketAsync(customerBasket.CustomerId);
        }

        public async Task<ApiResultDTO<CustomerBasketDTO>> AddItemToBasket(BasketItemDTO basketItemDTO)
        {
            var saveItem = new ApiResultDTO<CustomerBasketDTO> { Result = new CustomerBasketDTO() };

            #region stock control

            var isProductStockExist =
              _basketService.CheckProductStock(new CheckProductStockDTO
              {
                  ProductId = basketItemDTO.ProductId,
                  Quantity = basketItemDTO.Quantity
              });

            if (!isProductStockExist)
            {
                //If product is not enough or not exist
                saveItem.ResultType = DTO.ResultType.NotFound;
                saveItem.Message = "Not enough product";
                return saveItem;
            }

            #endregion

            var existBasketItems = await GetBasketAsync(basketItemDTO.CustomerId);

            //if there is not basket in Redis
            if (existBasketItems != null && 
                existBasketItems.BasketItems != null && 
                existBasketItems.BasketItems.Any())
            {

                //if same product added to card then it will sum existing and new quantity
                if (existBasketItems.BasketItems.Any(x => x.ProductId == basketItemDTO.ProductId))
                {
                    var existingProduct = existBasketItems
                        .BasketItems.First(x => x.ProductId == basketItemDTO.ProductId);

                    existingProduct.Quantity += basketItemDTO.Quantity;
                }
                else
                {
                    existBasketItems.BasketItems.Add(basketItemDTO);
                }

                saveItem.Result = existBasketItems;
                saveItem.Message = "Item successfully added.";
            }
            else
            {
                saveItem.Result.CustomerId = basketItemDTO.CustomerId;
                saveItem.Result.BasketItems.Add(basketItemDTO);
                saveItem.Message = "Item successfully added.";
            }

            saveItem.Result = await UpdateBasketAsync(saveItem.Result);

            return saveItem;
        }

        public async Task DeleteBasketAsync(string cacheKey)
        {
           await _distributedCache.RemoveAsync(cacheKey);
        }

    }
}
