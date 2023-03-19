using BMS.CoreBusiness.ViewModels.Membership;
using BMS.UseCases.PluginIRepositories.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BMS.Plugins.EFCore.Repositories.Membership
{
    internal sealed class RoleManagerRepository : IRoleManagerRepository
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly RoleManager<IdentityRole> _roleManager;
        private bool _busy;

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
            if (_busy) { return Enumerable.Empty<RoleViewModel>(); }

            _busy = true;
            try
            {
                if (_roleManager is null || _roleManager.Roles is null) { return Enumerable.Empty<RoleViewModel>(); }

                var roleList = _roleManager.Roles.ToList();

                return from role in roleList
                       select new RoleViewModel
                       {
                           RoleName = role.Name,
                           Id = role.Id,
                       };
            }
            catch (DbUpdateConcurrencyException)
            {

                _busy = false;
                throw;
            }
            catch (Exception)
            {
                _busy = false;
                throw;
            }
            finally
            {
                _busy = false;
            }
        }
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
