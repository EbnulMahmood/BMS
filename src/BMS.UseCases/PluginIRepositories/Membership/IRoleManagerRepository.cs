using BMS.CoreBusiness.ViewModels.Membership;

namespace BMS.UseCases.PluginIRepositories.Membership
{
    public interface IRoleManagerRepository
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
}
