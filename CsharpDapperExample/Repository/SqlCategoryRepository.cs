using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CsharpDapperExample.Repository
{
    public class SqlCategoryRepository : IRepository<Category>
    {
        private readonly string _connectionString;
        public SqlCategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }
        private IDbConnection Connection => new NpgsqlConnection(_connectionString);
        public async Task AddAsync(Category category)
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"INSERT INTO category (Name) VALUES (@Name)";

            await dbConnection.ExecuteAsync(sQuery, category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"SELECT * FROM category";

            var result = await dbConnection.QueryAsync<Category>(sQuery);
            return result;
        }
        
        public async Task<Category> GetByIdAsync(int id)
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"SELECT * FROM category WHERE Id = @Id";

            var result = await dbConnection.QueryFirstOrDefaultAsync<Category>(sQuery, new {Id = id});
            return result;
        }
        public async Task DeleteAsync(int id)
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"DELETE FROM category WHERE Id = @Id";

            await dbConnection.ExecuteAsync(sQuery, new { Id = id });
        }
        public async Task UpdateAsync(Category category)
        {
            using IDbConnection dbConnection = Connection;
            var sQuery = @"UPDATE category SET Name = @Name WHERE Id = @Id";

            await dbConnection.QueryAsync(sQuery, category);
        }
    }
}