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
    public class CategoryServiceStepDefinition
    {
        private readonly Mock<IRepository<Category>> _categoryRepoMock = new Mock<IRepository<Category>>();
        private CategoryService _categoryService;
        private IEnumerable<Category> _categories;
        private Category _category;
        private Category _gottenCategory;
        private Category _updatedCategory;

        [Given(@"categories")]
        public void GivenCategories()
        {
            _categoryRepoMock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(GetCategories);
            _categoryService = new CategoryService(_categoryRepoMock.Object);
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

        [When(@"get categories using CategoryService")]
        public async Task WhenGetCategoriesUsingCategoryService()
        {
            _categories = await _categoryService.GetAllCategoriesAsync();
        }

        [Then(@"categories should be the same")]
        public void ThenCategoriesShouldBeTheSame()
        {
            var result = Assert.IsAssignableFrom<IEnumerable<Category>>(_categories);
            Assert.Equal(GetCategories().Count(), result.Count());
            
            for (int i = 0; i < GetCategories().Count(); i++)
            {
                Assert.Equal(GetCategories().ElementAt(i).Name, result.ElementAt(i).Name);
            }
        }

        [Given(@"new category")]
        public void GivenNewCategory()
        {
            _category = new Category { Id = 7, Name = "Food" };
            
            _categoryRepoMock.Setup(repo=>repo.GetByIdAsync(_category.Id)).ReturnsAsync(_category);
            _categoryService = new CategoryService(_categoryRepoMock.Object);
        }

        [When(@"create new category")]
        public async Task WhenCreateNewCategory()
        {
            await _categoryService.CreateCategoryAsync(_category);
            _gottenCategory = await _categoryService.GetCategoryByIdAsync(_category.Id);
        }

        [Then(@"category should be created")]
        public void ThenCategoryShouldBeCreated()
        {
            var result = Assert.IsAssignableFrom<Category>(_gottenCategory);
            Assert.Equal(result.Name, _category.Name);
        }

        [Given(@"old and new categories")]
        public void GivenOldAndNewCategories()
        {
            _category = new Category { Id = 3, Name = "Sport" };
            _updatedCategory = new Category { Id = 3, Name = "Health" };
            _categoryRepoMock.Setup(repo=>repo.GetByIdAsync(_updatedCategory.Id)).ReturnsAsync(_updatedCategory);
            _categoryService = new CategoryService(_categoryRepoMock.Object);
        }

        [When(@"update an existing category")]
        public async Task WhenUpdateAnExistingCategory()
        {
            await _categoryService.UpdateCategoryAsync(_updatedCategory);
            _gottenCategory = await _categoryService.GetCategoryByIdAsync(_category.Id);
        }

        [Then(@"category should be updated")]
        public void ThenCategoryShouldBeUpdated()
        {
            var result = Assert.IsAssignableFrom<Category>(_gottenCategory);
            Assert.Equal(result.Name, _updatedCategory.Name);
        }
    }
}