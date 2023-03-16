using BMS.CoreBusiness.ViewModels.Membership;

namespace BMS.UseCases.PluginIRepositories.Membership
{
    public interface IUserManagerRepository
    {
        #region Operational Function
        Task SaveUserAsync(UserViewModel userViewModel);
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
