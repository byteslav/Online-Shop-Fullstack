using AutoMapper;
using CsharpDapperExample.Entities;

namespace CsharpDapperExample.Mapper
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