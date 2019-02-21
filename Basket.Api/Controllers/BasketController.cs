using System.Threading.Tasks;
using Basket.Api.Filters;
using Basket.Api.Models;
using Basket.Core.DTO;
using Basket.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IRedisBasketService _redisBasketService;
        public BasketController(IRedisBasketService redisBasketService)
        {
            _redisBasketService = redisBasketService;
        }

        //GET api/[controller]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Shopping Card Api Start. ReadMe.txt dökümanına bakınız.");
        }

        [Route("addBasketItem")]
        [HttpPost, ActionFilter]
        public async Task<IActionResult> AddBasketItem([FromBody]BasketItemModel basketItemModel)
        {
            var basketItem = new BasketItemDTO();
            basketItem.InjectFrom(basketItemModel);

            var result = await _redisBasketService.AddItemToBasket(basketItem);
     
            return Ok(result);
        }
    }
}
