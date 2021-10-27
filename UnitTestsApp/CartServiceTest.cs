using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services;
using CsharpDapperExample.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace TestProject1
{
    public class CartServiceTest
    {
        private readonly Mock<IRepository<Product>> _productRepoMock = new();
        private readonly Mock<IHttpContextAccessor> _httpContextMock = new();

        public CartServiceTest()
        {
            var services = new ServiceCollection();
            //services.AddScoped<GuestService>(p => new GuestService());
            var httpContextAccessorMock = _httpContextMock.Object;
            httpContextAccessorMock.HttpContext = new DefaultHttpContext
            {
                RequestServices = services.BuildServiceProvider()
            };
        }

        // [Fact]
        // public async Task Get_All_Products_In_Cart()
        // {
        //     //Arrange
        //     _productRepoMock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(GetProducts);
        //     _httpContextMock.Setup(http => http.HttpContext.Session
        //         .Get<List<ShoppingCart>>(WebConstants.SessionCart))
        //         .Returns(GetShoppingCartList);
        //     var service = new CartService(_httpContextMock.Object, _productRepoMock.Object);
        //     
        //     //Act
        //     var allProducts = await service.GetAllProductsInCartAsync();
        //     
        //     //Assert
        //     Assert.Equal(GetShoppingCartList().Count, allProducts.Count());
        //     for (int i = 0; i < allProducts.Count(); i++)
        //     {
        //         Assert.Equal(GetProducts().ElementAt(i).Name, allProducts.ElementAt(i).Name);
        //     }
        //
        // }
        
        private List<ShoppingCart> GetShoppingCartList()
        {
            var shoppingCartList = new List<ShoppingCart>
            {
                new ShoppingCart { ProductId = 1 },
                new ShoppingCart { ProductId = 2 },
                new ShoppingCart { ProductId = 3 }
            };
            return shoppingCartList;
        }
        
        private IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product { Id=1, Name="Bread", Description= "Tasty", Price = 100},
                new Product { Id=2, Name="Milk", Description= "White", Price = 70},
                new Product { Id=3, Name="Salad", Description= "Fresh", Price = 30}
            };
            return products;
        }
    }
}