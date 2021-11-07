using System;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<GrpcService> _logger;

        public GrpcService(ILogger<GrpcService> logger, IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
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

        public override async Task GetAllProducts(GetAllProductsRequest request, IServerStreamWriter<ProductModel> responseStream, ServerCallContext context)
        {
            var productList = await _productRepository.GetAllAsync();
            foreach (var product in productList)
            {
                var productModel = _mapper.Map<ProductModel>(product);
                await responseStream.WriteAsync(productModel);
            }
        }
    }
}