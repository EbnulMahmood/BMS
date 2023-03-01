using BMS.Plugins.EFCore.Repositories;
using BMS.UseCases.Extensions;
using BMS.UseCases.PluginIRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.Plugins.EFCore.Extensions
{
    public static class BMSRepositoriesExtension
    {
        public static void AddBMSRepositories(this IServiceCollection services)
        {
            #region Register All Repositories
            services.AddScoped<ITaskRepository, TaskRepository>();
            #endregion
        }
    }
}
