using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CsharpDapperExample.Entities;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CsharpDapperExample.Data.Repository
{
    public class SqlProductRepository : IRepository<Product>
    {
        private readonly string _connectionString;
        public SqlProductRepository(IOptions<DataBaseInfo> options)
        {
            _connectionString = options.Value.ConnectionString;
        }
        private IDbConnection Connection => new NpgsqlConnection(_connectionString);
        public async Task AddAsync(Product product)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"INSERT INTO products (name, price, description, categoryid) VALUES (@Name, @Price, @Description, @CategoryId)";

            await dbConnection.ExecuteAsync(sqlQuery, product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using IDbConnection dbConnection = Connection;
            
            var sqlQuery = @"SELECT p.id, p.name, p.price, p.description, p.categoryid, c.name FROM products p INNER JOIN category c ON p.categoryid = c.id";
            var result = await dbConnection.QueryAsync<Product, Category, Product>(sqlQuery, (product, category) =>
            {
                product.Category = category;
                return product;
            }, splitOn: "categoryid");
            
            return result;
        }
        
        public async Task<Product> GetByIdAsync(int id)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"SELECT p.id, p.name, p.price, p.description, p.categoryid, c.name FROM products p INNER JOIN category c ON p.categoryid = c.id WHERE p.id = @Id";
            var result = await dbConnection.QueryAsync<Product, Category, Product>(sqlQuery, (product, category) =>
            {
                product.Category = category;
                return product;
            }, splitOn: "categoryid", param: new {Id = id});

            return result.FirstOrDefault();
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
            var sqlQuery = @"UPDATE products SET Name = @Name, Price = @Price, Description = @Description, categoryid = @CategoryId WHERE Id = @Id";

            await dbConnection.QueryAsync(sqlQuery, product);
        }
    }
}