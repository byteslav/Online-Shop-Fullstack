using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services;
using CsharpDapperExample.ViewModels;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1
{
    public class ProductServiceTest
    {
        private readonly Mock<IRepository<Product>> _productRepoMock = new Mock<IRepository<Product>>();
        private readonly Mock<IRepository<Category>> _categoryRepoMock = new Mock<IRepository<Category>>();

        [Fact]
        public async Task Get_All_Products()
        {
            //Arrange
            _productRepoMock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(GetProducts);
            var service = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);
            
            // Act
            var products = await service.GetAllProductsAsync();
 
            // Assert
            var result = Assert.IsAssignableFrom<IEnumerable<Product>>(products);
            Assert.Equal(GetProducts().Count(), result.Count());
            
            for (int i = 0; i < GetProducts().Count(); i++)
            {
                Assert.Equal(GetProducts().ElementAt(i).Name, result.ElementAt(i).Name);
            }
        }

        [Fact]
        public async Task Create_New_Product()
        {
            //Arrange
            var product = new Product { Id = 1, Name = "Cookie", Description = "Yummy", CategoryId = 1 };
            
            _productRepoMock.Setup(repo=>repo.GetByIdAsync(product.Id)).ReturnsAsync(product);
            var service = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);
            
            // Act
            await service.CreateProductAsync(product);
            var gottenProduct = await service.GetProductByIdAsync(product.Id);
 
            // Assert
            var result = Assert.IsAssignableFrom<Product>(gottenProduct);
            Assert.Equal(result.Name, product.Name);
        }

        [Fact]
        public async Task Update_Product()
        {
            var productViewModel = new ProductViewModel { Product = new Product { Id = 2 } };
            var updatedProductViewModel = new ProductViewModel { Product = new Product { Id = 2, Name = "Pills", Description = "Healthy", CategoryId = 1 }};
            
            _productRepoMock.Setup(repo=>repo.GetByIdAsync(productViewModel.Product.Id)).ReturnsAsync(updatedProductViewModel.Product);
            var service = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);
            
            // Act
            await service.UpdateProductAsync(updatedProductViewModel.Product);
            var gottenProduct = await service.GetProductViewModelByIdAsync(productViewModel.Product.Id);

            // Assert
            var result = Assert.IsAssignableFrom<Product>(gottenProduct.Product);
            Assert.Equal(result.Name, updatedProductViewModel.Product.Name);
        }
        
        // [Fact]
        // public async Task Delete_Product()
        // {
        //     //Arrange
        //     var product = new Product { Id = 1 };
        //     _productRepoMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);
        //     var service = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);
        //     
        //     // Act
        //     var gottenProduct = await service.GetProductByIdAsync(product.Id);
        //     await service.DeleteProductAsync(product.Id);
        //     var deletedProduct = await service.GetProductByIdAsync(product.Id);
        //
        //     // Assert
        //     Assert.NotNull(gottenProduct);
        //     Assert.Null(deletedProduct);
        // }

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