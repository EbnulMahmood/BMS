using BMS.UseCases.Services;
using BMS.UseCases.Services.Membership;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.UseCases.Extensions
{
    public static class BMSServicesExtension
    {
        public static void AddBMSServices(this IServiceCollection services)
        {
            #region Register All Services
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IRoleManagerService, RoleManagerService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IProjectService, ProjectService>();
            #endregion
        }
    }
}
