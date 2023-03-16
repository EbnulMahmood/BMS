using BMS.CoreBusiness.Entities.Membership;
using BMS.CoreBusiness.ViewModels.Membership;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }

    public sealed class UserManagerService : IUserManagerService
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagerService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Operational Function
        public async Task SaveUserAsync(UserViewModel userViewModel)
        {
            try
            {
                string message = "Something Went Wrong!";

                var applicationUser = new ApplicationUser
                {
                    UserName = userViewModel.LoginName,
                    Email = userViewModel.LoginName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(applicationUser, "@E0b0nul");

                if (result.Succeeded)
                {
                    message = "New User Added!";
                    var role = _roleManager.Roles.FirstOrDefault(x => x.Id == userViewModel.UserRole);
                    var roleAddedResult = await _userManager.AddToRoleAsync(applicationUser, role?.Name ?? "User");
                    if (roleAddedResult.Succeeded) { message = "New User and Role Added!"; }

                    var claimAddedResult = await _userManager.AddClaimAsync(applicationUser, new Claim(role?.Name ?? "User", role?.Id ?? ""));
                    if (claimAddedResult.Succeeded) { message = "New User, Role and Claim Added!"; }
                }
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
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
