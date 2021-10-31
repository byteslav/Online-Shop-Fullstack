using System.Reflection;
using CsharpDapperExample.Models;
using CsharpDapperExample.Repository;
using CsharpDapperExample.Services;
using CsharpDapperExample.Services.Interfaces;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsharpDapperExample.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Product>, SqlProductRepository>();
            services.AddScoped<IRepository<Category>, SqlCategoryRepository>();
        }
        
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<ICartService, CartService>();
        }
        
        public static void AddMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(configure =>
                    configure.AddPostgres()
                        .WithGlobalConnectionString(configuration.GetValue<string>("DBInfo:ConnectionString"))
                        .ScanIn(Assembly.GetExecutingAssembly()).For.All())
                .AddLogging(configure => configure.AddFluentMigratorConsole());
        }
    }
}