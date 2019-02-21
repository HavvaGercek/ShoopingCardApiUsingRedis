using Basket.Api.Controllers;
using Basket.Api.Models;
using Basket.Core.DTO;
using Basket.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Tests
{
    public class BasketControllerTest
    {
        private readonly Mock<IRedisBasketService> _redisBasketServiceMock;
        private readonly Mock<IBasketService> _basketServiceMock;
        private readonly Mock<HttpContext> _contextMock;

        public BasketControllerTest()
        {
            _redisBasketServiceMock = new Mock<IRedisBasketService>();
            _basketServiceMock = new Mock<IBasketService>();
            _contextMock = new Mock<HttpContext>();
        }

        [Fact]
        public async Task Post_cart_success()
        {
            //Arrange
            var fakeCustomerId = "1";
            var fakeBasket = GetFakeBasket(fakeCustomerId);
            
            var fakeCheckProduct = new BasketItemDTO
            {
               ProductId = "fakeProduct",
               ProductName = "fakeItem",
               UnitPrice = 500,
               Quantity = 1,
               CustomerId = fakeCustomerId
            };

            fakeBasket.Result.BasketItems.Add(fakeCheckProduct);

            //Setting services
            _basketServiceMock.Setup(x => x.CheckProductStock(It.IsAny<CheckProductStockDTO>()))
                .Returns(true);

            _redisBasketServiceMock.Setup(x => x.AddItemToBasket(It.IsAny<BasketItemDTO>()))
                .Returns(Task.FromResult(fakeBasket));

            //Act
            var basketController = new BasketController(_redisBasketServiceMock.Object);
            basketController.ControllerContext.HttpContext = _contextMock.Object;

            var actionResult = await basketController.AddBasketItem(new BasketItemModel());

            
            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);

            Assert.Equal(viewResult.StatusCode,StatusCodes.Status200OK);
        }
        
        private ApiResultDTO<CustomerBasketDTO> GetFakeBasket(string customerId)
        {
            return new ApiResultDTO<CustomerBasketDTO>
            {
                Result = new CustomerBasketDTO
                {  
                    CustomerId = customerId,
                    BasketItems = new List<BasketItemDTO>()
                }
            }; 
        }
        
    }
}

