using BMS.CoreBusiness.Dtos;
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
        Task<IEnumerable<ResponsibleDeveloperDto>> LoadUserAsync(string roleId);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
