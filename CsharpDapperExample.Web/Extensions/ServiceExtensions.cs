using System.Reflection;
using CsharpDapperExample.BLL.Grpc;
using CsharpDapperExample.BLL.Grpc.Services;
using CsharpDapperExample.BLL.Interfaces;
using CsharpDapperExample.BLL.Services;
using CsharpDapperExample.Data;
using CsharpDapperExample.Data.Repository;
using CsharpDapperExample.Entities;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsharpDapperExample.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataBaseInfo>(options => configuration.GetSection("DataBaseInfo").Bind(options));
            services.AddScoped<IRepository<Product>, SqlProductRepository>();
            services.AddScoped<IRepository<Category>, SqlCategoryRepository>();
        }
        
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<ProductGrpcService>();
            services.AddScoped<CategoryGrpcService>();
        }
        
        public static void AddMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(configure =>
                    configure.AddPostgres()
                        .WithGlobalConnectionString(configuration.GetValue<string>("DataBaseInfo:ConnectionString"))
                        .ScanIn(Assembly.GetExecutingAssembly()).For.All())
                .AddLogging(configure => configure.AddFluentMigratorConsole());
        }
    }
}