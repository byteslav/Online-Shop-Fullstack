using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CsharpDapperExample.Data.Repository;
using CsharpDapperExample.Entities;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CsharpDapperExample.Grpc
{
    public class ProductGrpcService : Products.ProductsBase
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductGrpcService> _logger;

        public ProductGrpcService(ILogger<ProductGrpcService> logger, IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<ProductModel> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID={request.ProductId} is not found."));
            }
            var productModel = _mapper.Map<ProductModel>(product);
            return productModel;
        }

        public override async Task<AllProductsResponse> GetAllProducts(GetAllProductsRequest request, ServerCallContext context)
        {
            var products = await _productRepository.GetAllAsync();
            var productModels = products.Select(p => _mapper.Map<ProductModel>(p));
            
            var response = new AllProductsResponse();
            response.ProductModel.AddRange(productModels);
            return response;
        }

        public override async Task<ProductModel> AddProduct(AddProductRequest request, ServerCallContext context)
        {
            var product = _mapper.Map<Product>(request.Product);
            await _productRepository.AddAsync(product);
            
            return request.Product;
        }

        public override async Task<ProductModel> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var product = _mapper.Map<Product>(request.Product);
            await _productRepository.UpdateAsync(product);
            
            return request.Product;
        }

        public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            await _productRepository.DeleteAsync(request.ProductId);
            return new DeleteProductResponse();
        }
    }
}