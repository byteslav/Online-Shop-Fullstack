using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CsharpDapperExample.Models;
using CsharpDapperExample.Utility;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CsharpDapperExample.Repository
{
    public class SqlCategoryRepository : IRepository<Category>
    {
        private readonly string _connectionString;
        public SqlCategoryRepository(IOptions<DataBaseInfo> options)
        {
            _connectionString = options.Value.ConnectionString;
        }
        private IDbConnection Connection => new NpgsqlConnection(_connectionString);
        public async Task AddAsync(Category category)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"INSERT INTO category (Name) VALUES (@Name)";

            await dbConnection.ExecuteAsync(sqlQuery, category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"SELECT * FROM category";

            var result = await dbConnection.QueryAsync<Category>(sqlQuery);
            return result;
        }
        
        public async Task<Category> GetByIdAsync(int id)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"SELECT * FROM category WHERE Id = @Id";

            var result = await dbConnection.QueryFirstOrDefaultAsync<Category>(sqlQuery, new {Id = id});
            return result;
        }
        public async Task DeleteAsync(int id)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"DELETE FROM category WHERE Id = @Id";

            await dbConnection.ExecuteAsync(sqlQuery, new { Id = id });
        }
        public async Task UpdateAsync(Category category)
        {
            using IDbConnection dbConnection = Connection;
            var sqlQuery = @"UPDATE category SET Name = @Name WHERE Id = @Id";

            await dbConnection.QueryAsync(sqlQuery, category);
        }
    }
}