using AutoMapper;
using CsharpDapperExample.Entities;

namespace CsharpDapperExample.BLL.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
        }
    }
}