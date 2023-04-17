using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BMS.Plugins.Dapper.Data
{
    public interface IRelationalDataAccess
    {
        Task<IEnumerable<TEntity>> LoadDataAsync<TEntity, TParam>(string sql, TParam parameters, CommandType commandType = default);
        Task<TEntity> GetFirstOrDefaultDataAsync<TEntity, TParam>(string sql, TParam parameters, CommandType commandType = default);
        Task SaveDataAsync<TParam>(string sql, TParam parameters, CommandType commandType = default);
    }

    internal sealed class RelationalDataAccess : IRelationalDataAccess
    {
        private readonly IConfiguration _config;
        private const string _conntectionString = "BMSDbContext";
        public RelationalDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<TEntity>> LoadDataAsync<TEntity, TParam>(string sql, TParam parameters, CommandType commandType = default)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_conntectionString));

                return await connection.QueryAsync<TEntity>(sql, parameters, commandType: commandType);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity> GetFirstOrDefaultDataAsync<TEntity, TParam>(string sql, TParam parameters, CommandType commandType = default)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_conntectionString));

                return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters, commandType: commandType);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SaveDataAsync<TParam>(string sql, TParam parameters, CommandType commandType = default)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_conntectionString));

                await connection.ExecuteAsync(sql, parameters, commandType: commandType);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
