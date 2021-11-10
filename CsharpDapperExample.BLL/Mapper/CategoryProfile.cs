using AutoMapper;
using CsharpDapperExample.Entities;

namespace CsharpDapperExample.BLL.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
        }
    }
}