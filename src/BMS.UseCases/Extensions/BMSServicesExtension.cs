using BMS.UseCases.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.UseCases.Extensions
{
    public static class BMSServicesExtension
    {
        public static void AddBMSServices(this IServiceCollection services)
        {
            #region Register All Services
            services.AddScoped<ITaskService, TaskService>();
            #endregion
        }
    }
}
