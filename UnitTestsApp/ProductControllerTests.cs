using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Controllers;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

namespace TestProject1
{
    public class ProductControllerTests
    {
        private readonly Mock<IRepository<Product>> _mock = new Mock<IRepository<Product>>();
        [Fact]
        public async Task IndexReturnsAViewResultWithAllProducts()
        {
            //Arrange
            _mock.Setup(repo=>repo.GetAllAsync()).Returns(GetProducts);
            var controller = new ProductsController(_mock.Object);
            
            // Act
            var result = await controller.Index();
 
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(GetProducts().Result.Count(), model.Count());
        }
        
        private async Task<IEnumerable<Product>> GetProducts()
        {
            var products = new List<Product>
            {
                new Product { Id=1, Name="Bread", Count= 35, Price = 100},
                new Product { Id=2, Name="Milk", Count= 29, Price = 70},
                new Product { Id=3, Name="Salad", Count= 32, Price = 30}
            };
            return products;
        }
        
        [Fact]
        public async Task AddProductReturnsViewResultWithProductModel()
        {
            // Arrange
            var controller = new ProductsController(_mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var newProduct = new Product();
 
            // Act
            var result = await controller.Create(newProduct);
 
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.Model);
            Assert.Equal(newProduct, model);
        }
        
        [Fact]
        public async Task DeleteBadRequestResultWhenIdIsNull()
        {
            // Arrange
            var controller = new ProductsController(_mock.Object);
 
            // Act
            var result = await controller.Delete(null);
 
            // Arrange
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public async Task CreateReturnsARedirectAndAdd()
        {
            // Arrange
            var controller = new ProductsController(_mock.Object);
            var newProduct = new Product { Name = "Tea" };
 
            // Act
            var result = await controller.Create(newProduct);
 
            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mock.Verify(repository => repository.AddAsync(newProduct));
        }
        
        [Fact]
        public async Task CreateReturnsViewResultWithProductModel()
        {
            // Arrange
            var controller = new ProductsController(_mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var newProduct = new Product();
 
            // Act
            var result = await controller.Create(newProduct);
 
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newProduct, viewResult?.Model);
        }
    }
}