using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.BLL.Services;
using CsharpDapperExample.Data.Repository;
using CsharpDapperExample.Entities;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace CsharpDapperExample.Specs.Steps
{
    [Binding]
    public class ProductServiceStepDefinitions
    {
        private readonly Mock<IRepository<Product>> _productRepoMock = new Mock<IRepository<Product>>();
        private readonly Mock<IRepository<Category>> _categoryRepoMock = new Mock<IRepository<Category>>();
        private ProductService _productService;
        private IEnumerable<Product> _products;
        private Product _product;
        private Product _gottenProduct;
        private Product _updatedProduct;
        
        [Given(@"products")]
        public void GivenProducts()
        {
            _productRepoMock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(GetProducts);
            _productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);
        }

        [When(@"get products using ProductService")]
        public async Task WhenProductsWereGettingWithProductService()
        {
            _products = await _productService.GetAllProductsAsync();
        }

        [Then(@"products should be the same")]
        public void ThenProductsShouldBeTheSame()
        {
            var result = Assert.IsAssignableFrom<IEnumerable<Product>>(_products);
            
            Assert.Equal(GetProducts().Count(), result.Count());
            for (int i = 0; i < GetProducts().Count(); i++)
            {
                Assert.Equal(GetProducts().ElementAt(i).Name, result.ElementAt(i).Name);
            }
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

        [Given(@"new product")]
        public void GivenNewProduct()
        {
            _product = new Product { Id = 1, Name = "Cookie", Description = "Yummy", CategoryId = 1 };
            _productRepoMock.Setup(repo=>repo.GetByIdAsync(_product.Id)).ReturnsAsync(_product);
            _productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);
        }

        [When(@"create new product")]
        public async Task WhenCreateNewProduct()
        {
            await _productService.CreateProductAsync(_product);
            _gottenProduct = await _productService.GetProductByIdAsync(_product.Id);
        }

        [Then(@"product should be created")]
        public void ThenProductShouldBeCreated()
        {
            var result = Assert.IsAssignableFrom<Product>(_gottenProduct);
            Assert.Equal(result.Name, _product.Name);
        }

        [Given(@"old and new products")]
        public void GivenOldAndNewProducts()
        {
            _product = new Product { Id = 3, Name = "Cookie", Description = "Tasty", Price = 100};
            _updatedProduct = new Product { Id = 3, Name = "Cookie", Description = "Very Tasty", Price = 75 };
            _productRepoMock.Setup(repo=>repo.GetByIdAsync(_updatedProduct.Id)).ReturnsAsync(_updatedProduct);
            _productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object);

        }

        [When(@"update an existing product")]
        public async Task WhenUpdateAnExistingProduct()
        {
            await _productService.UpdateProductAsync(_updatedProduct);
            _gottenProduct = await _productService.GetProductByIdAsync(_product.Id);
        }

        [Then(@"product should be updated")]
        public void ThenProductShouldBeUpdated()
        {
            var result = Assert.IsAssignableFrom<Product>(_gottenProduct);
            Assert.Equal(result.Name, _updatedProduct.Name);
        }
    }
}