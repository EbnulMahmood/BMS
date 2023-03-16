using BMS.Plugins.EFCore.Repositories;
using BMS.Plugins.EFCore.Repositories.Membership;
using BMS.UseCases.PluginIRepositories;
using BMS.UseCases.PluginIRepositories.Membership;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.Plugins.EFCore.Extensions
{
    public static class BMSRepositoriesExtension
    {
        public static void AddBMSRepositories(this IServiceCollection services)
        {
            #region Register All Repositories
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<IRoleManagerRepository, RoleManagerRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            #endregion
        }
    }
}
