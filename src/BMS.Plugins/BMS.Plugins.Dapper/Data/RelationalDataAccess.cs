using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace BMS.Plugins.Dapper.Data
{
    public interface IRelationalDataAccess
    {
        #region Operational Function
        Task SaveDataAsync<TParam>(string sql, TParam parameters, CommandType commandType = default);
        #endregion

        #region Single Instances Loading Function
        Task<TEntity> GetFirstOrDefaultDataAsync<TEntity, TParam>(string sql, TParam parameters, CommandType commandType = default);
        #endregion

        #region List Loading Function
        Task<IEnumerable<TEntity>> LoadDataAsync<TEntity, TParam>(string sql, TParam parameters, CommandType commandType = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }

    internal sealed class RelationalDataAccess : IRelationalDataAccess
    {
        #region Logger
        private readonly ILogger<RelationalDataAccess> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly IConfiguration _config;
        private const string _conntectionString = "BMSDbContext";
        public RelationalDataAccess(IConfiguration config, ILogger<RelationalDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        }
        #endregion

        #region Operational Function
        public async Task SaveDataAsync<TParam>(string sql, TParam parameters, CommandType commandType = default)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_conntectionString));

                await connection.ExecuteAsync(sql, parameters, commandType: commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(SaveDataAsync));
                throw;
            }
        }
        #endregion

        #region Single Instances Loading Function
        public async Task<TEntity> GetFirstOrDefaultDataAsync<TEntity, TParam>(string sql, TParam parameters, CommandType commandType = default)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_conntectionString));

                return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters, commandType: commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(GetFirstOrDefaultDataAsync));
                throw;
            }
        }
        #endregion

        #region List Loading Function
        public async Task<IEnumerable<TEntity>> LoadDataAsync<TEntity, TParam>(string sql, TParam parameters, CommandType commandType = default)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_conntectionString));

                return await connection.QueryAsync<TEntity>(sql, parameters, commandType: commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadDataAsync));
                throw;
            }
        }
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
