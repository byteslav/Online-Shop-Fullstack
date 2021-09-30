using System.Collections.Generic;
using System.Data;
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

        public void Add(Product product)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var sQuery = @"INSERT INTO Products (Name, Count, Price) VALUES (@Name, @Count, @Price)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, product);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                var sQuery = @"SELECT * FROM Products";
                dbConnection.Open();
                return dbConnection.Query<Product>(sQuery);
            }
        }
        
        public Product GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var sQuery = @"SELECT * FROM Products WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Product>(sQuery, new {Id = id});
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var sQuery = @"DELETE FROM Products WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }
        public void Update(Product product)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var sQuery = @"UPDATE Products SET Name = @Name, Count = @Count, Price = @Price WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Query(sQuery, product);
            }
        }
    }
}