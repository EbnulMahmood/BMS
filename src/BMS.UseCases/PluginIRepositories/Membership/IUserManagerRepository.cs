﻿using BMS.CoreBusiness.Dtos;
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
        Task<IEnumerable<ResponsibleUserDto>> LoadUserAsync(string roleId, CancellationToken token = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
