using BMS.CoreBusiness.ViewModels.Membership;
using Microsoft.AspNetCore.Identity;

namespace BMS.UseCases.Services.Membership
{
    public interface IRoleManagerService
    {
        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        IEnumerable<RoleViewModel> LoadRole();
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }

    public sealed class RoleManagerService : IRoleManagerService
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerService(RoleManager<IdentityRole> roleManager)
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
