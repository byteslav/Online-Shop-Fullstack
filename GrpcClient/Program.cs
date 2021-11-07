using System;
using System.Threading;
using System.Threading.Tasks;
using CsharpDapperExample;
using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server is running");
            Thread.Sleep(200);

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Products.ProductsClient(channel);

            var products = client.GetAllProducts(new GetAllProductsRequest());
            Console.WriteLine(products);
        }
    }
}