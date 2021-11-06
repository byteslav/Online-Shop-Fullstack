using System;
using System.Threading;
using System.Threading.Tasks;
using CsharpDapperExample;
using Grpc.Core;
using Grpc.Net.Client;
using static CsharpDapperExample.Greeter;

namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server is running");
            Thread.Sleep(2000);

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GreeterClient(channel);

            PrintSomething(client);
            GetAllProductsAsync(client);
        }

        private static void PrintSomething(GreeterClient client)
        {
            Console.WriteLine("Print method started...");
            var response = client.SayHello(new HelloRequest
            {
                Name = "Bill"
            });

            Console.WriteLine("Print method Response: " + response);
            Thread.Sleep(1000);
        }

        private static async Task GetAllProductsAsync(GreeterClient client)
        {
            Console.WriteLine("Get All Products method is started");
            using var clientData = client.GetAllProducts(new GetAllProductsRequest());
            
            await foreach (var responseData in clientData.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(responseData);
            }
        }
    }
}