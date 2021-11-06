using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Data.Repository;
using CsharpDapperExample.Entities;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using static CsharpDapperExample.Greeter;

namespace CsharpDapperExample.Grpc
{
    public class GrpcService : GreeterBase
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ILogger<GrpcService> _logger;

        public GrpcService(ILogger<GrpcService> logger, IRepository<Product> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task GetAllProducts(GetAllProductsRequest request, IServerStreamWriter<ProductModel> responseStream, ServerCallContext context)
        {
            var productList = await _productRepository.GetAllAsync();
            for (int i = 1; i <= productList.Count(); i++)
            {
                var product = await _productRepository.GetByIdAsync(i);
                var productModel = new ProductModel
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId
                };
                await responseStream.WriteAsync(productModel);
            }
        }
    }
}