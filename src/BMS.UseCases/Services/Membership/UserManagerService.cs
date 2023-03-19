using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.ViewModels.Membership;
using BMS.UseCases.PluginIRepositories.Membership;

namespace BMS.UseCases.Services.Membership
{
    public interface IUserManagerService
    {
        #region Operational Function
        Task SaveUserAsync(UserViewModel userViewModel);
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        Task<IEnumerable<ResponsibleUserDto>> LoadUserAsync(string roleId, CancellationToken token = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }

    internal sealed class UserManagerService : IUserManagerService
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly IUserManagerRepository _userManagerRepository;

        public UserManagerService(IUserManagerRepository userManagerRepository)
        {
            _userManagerRepository = userManagerRepository;
        }
        #endregion

        #region Operational Function
        public async Task SaveUserAsync(UserViewModel userViewModel)
        {
            try
            {
                await _userManagerRepository.SaveUserAsync(userViewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        public async Task<IEnumerable<ResponsibleUserDto>> LoadUserAsync(string roleId, CancellationToken token = default)
        {
            try
            {
                return await _userManagerRepository.LoadUserAsync(roleId, token);
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
