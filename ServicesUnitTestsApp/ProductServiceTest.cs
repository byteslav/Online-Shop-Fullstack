using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.BLL.Services;
using CsharpDapperExample.Data.Repository;
using CsharpDapperExample.Entities;
using Moq;
using Xunit;

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
            // Arrange
            var product = new Product { Id = 3, Name = "Cookie", Description = "Tasty", Price = 100};
            var updatedProduct = new Product { Id = 3, Name = "Cookie", Description = "Very Tasty", Price = 75 };
            _productRepoMock.Setup(repo=>repo.GetByIdAsync(updatedProduct.Id)).ReturnsAsync(updatedProduct);
            var service = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);
            
            // Act
            await service.UpdateProductAsync(updatedProduct);
            var gottenProduct = await service.GetProductByIdAsync(product.Id);

            // Assert
            var result = Assert.IsAssignableFrom<Product>(gottenProduct);
            Assert.Equal(result.Name, updatedProduct.Name);
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