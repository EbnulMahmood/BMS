using BMS.Plugins.Dapper.Data;
using BMS.Plugins.Dapper.Repositories;
using BMS.UseCases.PluginIRepositories.Query;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.Plugins.Dapper.Extensions
{
    public static class DapperRepositoriesExtension
    {
        public static void AddDapperRepositories(this IServiceCollection services)
        {
            #region Register DataAccess
            services.AddSingleton<IRelationalDataAccess, RelationalDataAccess>();
            #endregion
            #region Register All Repositories
            services.AddScoped<IQueryProjectRepository, QueryProjectRepository>();
            services.AddScoped<IQueryTaskRepository, QueryTaskRepository>();
            #endregion
        }
    }
}
