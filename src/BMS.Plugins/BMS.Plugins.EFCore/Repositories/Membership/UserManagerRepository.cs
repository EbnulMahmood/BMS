using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities.Membership;
using BMS.CoreBusiness.ViewModels.Membership;
using BMS.Plugins.EFCore.Data;
using BMS.UseCases.PluginIRepositories.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml.Linq;

namespace BMS.Plugins.EFCore.Repositories.Membership
{
    internal sealed class UserManagerRepository : IUserManagerRepository
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly BMSDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagerRepository(BMSDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
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
        public async Task<IEnumerable<ResponsibleDeveloperDto>> LoadUserAsync(string roleName)
        {
            try
            {
                var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name.Contains(roleName.Trim()));

                if (role == null) throw new InvalidDataException(nameof(role));

                return await (from u in _context.Users
                              join ur in _context.UserRoles on u.Id equals ur.UserId into uur
                              from ur in uur.DefaultIfEmpty()
                              join r in _context.Roles on ur.RoleId equals r.Id into rur
                              from r in rur.DefaultIfEmpty()
                              where r.Id.Contains(role.Id.Trim())
                              select new ResponsibleDeveloperDto
                              {
                                  Id = u.Id,
                                  Name = u.UserName
                              }).ToListAsync();

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
