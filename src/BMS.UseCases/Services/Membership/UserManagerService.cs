using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.ViewModels.Membership;
using BMS.UseCases.PluginIRepositories.Membership;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<UserManagerService> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly IUserManagerRepository _userManagerRepository;

        public UserManagerService(IUserManagerRepository userManagerRepository,  ILogger<UserManagerService> logger)
        {
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}",  nameof(SaveUserAsync));
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method} with {RoleId}",  nameof(LoadUserAsync), roleId);
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
