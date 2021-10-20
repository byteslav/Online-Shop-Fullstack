using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CsharpDapperExample.Repository
{
    public class SqlProductRepository : IRepository<Product>
    {
        private readonly string _connectionString;
        public SqlProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }
        private IDbConnection Connection => new NpgsqlConnection(_connectionString);
        public async Task AddAsync(Product product)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"INSERT INTO products (name, count, price, categoryid) VALUES (@Name, @Count, @Price, @CategoryId)";

            await dbConnection.ExecuteAsync(sqlQuery, product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using IDbConnection dbConnection = Connection;
            
            var sqlQuery = @"SELECT p.id, p.name, p.count, p.price, p.categoryid, c.name FROM products p INNER JOIN category c ON p.categoryid = c.id";
            var result = await dbConnection.QueryAsync<Product, Category, Product>(sqlQuery, (product, category) => {
                    product.Category = category;
                    return product;
                }, splitOn: "categoryid");
            
            return result;
        }
        
        public async Task<Product> GetByIdAsync(int id)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"SELECT * FROM products WHERE Id = @Id";

            var result = await dbConnection.QueryFirstOrDefaultAsync<Product>(sqlQuery, new {Id = id});
            return result;
        }
        public async Task DeleteAsync(int id)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"DELETE FROM products WHERE Id = @Id";

            await dbConnection.ExecuteAsync(sqlQuery, new { Id = id });
        }
        public async Task UpdateAsync(Product product)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"UPDATE products SET Name = @Name, Count = @Count, Price = @Price, categoryid = @CategoryId WHERE Id = @Id";

            await dbConnection.QueryAsync(sqlQuery, product);
        }
    }
}