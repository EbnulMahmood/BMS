using BMS.CoreBusiness.ViewModels.Membership;
using BMS.UseCases.PluginIRepositories.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BMS.Plugins.EFCore.Repositories.Membership
{
    internal sealed class RoleManagerRepository : IRoleManagerRepository
    {
        #region Logger
        private readonly ILogger<RoleManagerRepository> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly RoleManager<IdentityRole> _roleManager;
        private bool _busy;

        public RoleManagerRepository(RoleManager<IdentityRole> roleManager
        , ILogger<RoleManagerRepository> logger)
        {
            _logger = logger;
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
                if (_roleManager is null || _roleManager.Roles is null)
                {
                    throw new NullReferenceException(nameof(_roleManager));
                }

                var roleList = _roleManager.Roles.ToList();

                return from role in roleList
                       select new RoleViewModel
                       {
                           RoleName = role.Name,
                           Id = role.Id,
                       };
            }
            catch (NullReferenceException)
            {
                _busy = false;
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadRole));
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
