using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using csharp_dapper_example.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace csharp_dapper_example.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }
        private IDbConnection Connection => new NpgsqlConnection(_connectionString);
        public async Task AddAsync(Product product)
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"INSERT INTO Products (Name, Count, Price) VALUES (@Name, @Count, @Price)";
            dbConnection.Open();
            
            await dbConnection.ExecuteAsync(sQuery, product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"SELECT * FROM Products";
            dbConnection.Open();
            
            var result = await dbConnection.QueryAsync<Product>(sQuery);
            return result;
        }
        
        public async Task<Product> GetByIdAsync(int? id)
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"SELECT * FROM Products WHERE Id = @Id";
            dbConnection.Open();
                
            var result = await dbConnection.QueryFirstOrDefaultAsync<Product>(sQuery, new {Id = id});
            return result;
        }
        public async Task DeleteAsync(int id)
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"DELETE FROM Products WHERE Id = @Id";
            dbConnection.Open();
            
            await dbConnection.ExecuteAsync(sQuery, new { Id = id });
        }
        public async Task UpdateAsync(Product product)
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"UPDATE Products SET Name = @Name, Count = @Count, Price = @Price WHERE Id = @Id";
            dbConnection.Open();
            
            await dbConnection.QueryAsync(sQuery, product);
        }
    }
}