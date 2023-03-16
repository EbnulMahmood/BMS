using BMS.CoreBusiness.ViewModels.Membership;
using BMS.UseCases.PluginIRepositories.Membership;

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

    internal sealed class RoleManagerService : IRoleManagerService
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly IRoleManagerRepository _roleManagerRepository;

        public RoleManagerService(IRoleManagerRepository roleManagerRepository)
        {
            _roleManagerRepository = roleManagerRepository;
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
                return _roleManagerRepository.LoadRole();
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
