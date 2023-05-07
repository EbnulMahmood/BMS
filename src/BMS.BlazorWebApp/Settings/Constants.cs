namespace BMS.BlazorWebApp.Settings
{
    public static class Constants
    {
        public static readonly string connectionStringName = "BMSDbContext";

        public static readonly string offlineAccess = "offline_access";

        public static readonly string errorPage = "/Error";
        public static readonly string hostPage = "/_Host";

        public static readonly string appsettingsFileName = "appsettings.json";

        public const int InitItemsPerPage = 10;
    }


    public static class PolicyConstants
    {
        public static readonly string SuperAdminOnly = "SuperAdminOnly";
        public static readonly string AdminOnly = "AdminOnly";
        public static readonly string DeveloperOnly = "DeveloperOnly";
        public static readonly string QAOnly = "QAOnly";
        public static readonly string UserOnly = "UserOnly";
    }

    public static class RoleConstants
    {
        public static readonly string SuperAdmin = "SuperAdmin";
        public static readonly string Admin = "Admin";
        public static readonly string Developer = "Developer";
        public static readonly string QA = "QA";
        public static readonly string User = "User";
    }
}
