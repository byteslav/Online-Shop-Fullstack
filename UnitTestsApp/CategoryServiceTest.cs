using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services;
using Moq;
using Xunit;

namespace TestProject1
{
    public class CategoryServiceTest
    {
        private readonly Mock<IRepository<Category>> _categoryRepoMock = new Mock<IRepository<Category>>();
        
        [Fact]
        public async Task Get_All_Categories()
        {
            //Arrange
            _categoryRepoMock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(GetCategories);
            var service = new CategoryService(_categoryRepoMock.Object);
            
            // Act
            var categories = await service.GetAllCategoriesAsync();
 
            // Assert
            var result = Assert.IsAssignableFrom<IEnumerable<Category>>(categories);
            Assert.Equal(GetCategories().Count(), result.Count());
            
            for (int i = 0; i < GetCategories().Count(); i++)
            {
                Assert.Equal(GetCategories().ElementAt(i).Name, result.ElementAt(i).Name);
            }
        }
        
        [Fact]
        public async Task Create_New_Category()
        {
            //Arrange
            var category = new Category { Id = 7, Name = "Food" };
            
            _categoryRepoMock.Setup(repo=>repo.GetByIdAsync(category.Id)).ReturnsAsync(category);
            var service = new CategoryService(_categoryRepoMock.Object);
            
            // Act
            await service.CreateCategoryAsync(category);
            var gottenCategory = await service.GetCategoryByIdAsync(category.Id);
 
            // Assert
            var result = Assert.IsAssignableFrom<Category>(gottenCategory);
            Assert.Equal(result.Name, category.Name);
        }
        
        [Fact]
        public async Task Update_Category()
        {
            var category = new Category { Id = 3, Name = "Sport" };
            var updatedCategory = new Category { Id = 3, Name = "Health" };
            
            _categoryRepoMock.Setup(repo=>repo.GetByIdAsync(updatedCategory.Id)).ReturnsAsync(updatedCategory);
            var service = new CategoryService(_categoryRepoMock.Object);
            
            // Act
            await service.UpdateCategoryAsync(updatedCategory);
            var gottenCategory = await service.GetCategoryByIdAsync(category.Id);

            // Assert
            var result = Assert.IsAssignableFrom<Category>(gottenCategory);
            Assert.Equal(result.Name, updatedCategory.Name);
        }
        
        private IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>
            {
                new Category { Id=1, Name="Groceries" },
                new Category { Id=2, Name="Sport" },
                new Category { Id=3, Name="Health" }
            };
            return categories;
        }
    }
}