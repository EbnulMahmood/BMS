namespace BMS.BlazorWebApp.Settings
{
    public static class AuthorizationSettings
    {
        public static void AddAuthorizationSettings(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConstants.SuperAdminOnly, policy =>
                    policy.RequireClaim(RoleConstants.SuperAdmin));
                options.AddPolicy(PolicyConstants.AdminOnly, policy =>
                    policy.RequireClaim(RoleConstants.Admin));
                options.AddPolicy(PolicyConstants.DeveloperOnly, policy =>
                    policy.RequireClaim(RoleConstants.Developer));
                options.AddPolicy(PolicyConstants.QAOnly, policy =>
                    policy.RequireClaim(RoleConstants.QA));
                options.AddPolicy(PolicyConstants.UserOnly, policy =>
                    policy.RequireClaim(RoleConstants.User));
            });
        }
    }
}
