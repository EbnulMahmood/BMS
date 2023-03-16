using BMS.CoreBusiness.ViewModels.Membership;
using BMS.UseCases.PluginIRepositories.Membership;
using Microsoft.AspNetCore.Identity;

namespace BMS.Plugins.EFCore.Repositories.Membership
{
    internal sealed class RoleManagerRepository : IRoleManagerRepository
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        #endregion

        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        public IEnumerable<RoleViewModel> LoadRole()
        {
            try
            {
                var roleList = _roleManager.Roles.ToList();

                return from role in roleList
                       select new RoleViewModel
                       {
                           RoleName = role.Name,
                           Id = role.Id,
                       };
            }
            catch (Exception)
            {

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
