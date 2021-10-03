using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using csharp_dapper_example.Controllers;
using csharp_dapper_example.Models;
using csharp_dapper_example.Repository;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

namespace TestProject1
{
    public class ProductControllerTests
    {
        [Fact]
        public async void Index_ReturnsAViewResultWithAllProducts()
        {
            //Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo=>repo.GetAllAsync()).Returns(GetProducts);
            var controller = new ProductController(mock.Object);
            
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
        public async void AddProduct_ReturnsViewResultWithProductModel()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            var controller = new ProductController(mock.Object);
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
        public async void Delete_BadRequestResultWhenIdIsNull()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            var controller = new ProductController(mock.Object);
 
            // Act
            var result = await controller.Delete(null);
 
            // Arrange
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public async void Create_ReturnsARedirectAndAdd()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            var controller = new ProductController(mock.Object);
            var newProduct = new Product
            {
                Name = "Tea"
            };
 
            // Act
            var result = await controller.Create(newProduct);
 
            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(repository => repository.AddAsync(newProduct));
        }
        
        [Fact]
        public async void Create_ReturnsViewResultWithProductModel()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            var controller = new ProductController(mock.Object);
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