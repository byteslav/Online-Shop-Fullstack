using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CsharpDapperExample.Data.Repository;
using CsharpDapperExample.Entities;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CsharpDapperExample.BLL.Grpc.Services
{
    public class CategoryGrpcService : Categories.CategoriesBase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryGrpcService> _logger;

        public CategoryGrpcService(ILogger<CategoryGrpcService> logger, IMapper mapper, IRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public override async Task<CategoryModel> GetCategory(GetCategoryRequest request, ServerCallContext context)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return categoryModel;
        }

        public override async Task<AllCategoriesResponse> GetAllCategories(GetAllCategoriesRequest request, ServerCallContext context)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryModels = categories.Select(c => _mapper.Map<CategoryModel>(c));
            
            var response = new AllCategoriesResponse();
            response.Categories.AddRange(categoryModels);
            return response;
        }

        public override async Task<CategoryModel> AddCategory(AddCategoryRequest request, ServerCallContext context)
        {
            var category = _mapper.Map<Category>(request.Category);
            await _categoryRepository.AddAsync(category);
            
            return request.Category;
        }

        public override async Task<CategoryModel> UpdateCategory(UpdateCategoryRequest request, ServerCallContext context)
        {
            var category = _mapper.Map<Category>(request.Category);
            await _categoryRepository.UpdateAsync(category);
            
            return request.Category;
        }

        public override async Task<DeleteCategoryResponse> DeleteCategory(DeleteCategoryRequest request, ServerCallContext context)
        {
            await _categoryRepository.DeleteAsync(request.CategoryId);
            return new DeleteCategoryResponse();
        }
    }
}