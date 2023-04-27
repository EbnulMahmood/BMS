using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities.Membership;
using BMS.CoreBusiness.ViewModels.Membership;
using BMS.Plugins.EFCore.Data;
using BMS.UseCases.PluginIRepositories.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BMS.Plugins.EFCore.Repositories.Membership
{
    internal sealed class UserManagerRepository : IUserManagerRepository
    {
        #region Logger
        private readonly ILogger<UserManagerRepository> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDbContextFactory<BMSDbContext> _contextFactory;
        private bool _busy;

        public UserManagerRepository(IDbContextFactory<BMSDbContext> contextFactory
        , UserManager<ApplicationUser> userManager
        , RoleManager<IdentityRole> roleManager
        , ILogger<UserManagerRepository> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _contextFactory = contextFactory;
        }
        #endregion

        #region Operational Function
        public async Task SaveUserAsync(UserViewModel userViewModel)
        {
            if (_busy) { return; }

            _busy = true;
            try
            {
                string message = "Something Went Wrong!";

                var applicationUser = new ApplicationUser
                {
                    UserName = userViewModel.LoginName,
                    Email = userViewModel.LoginName,
                    EmailConfirmed = true
                };

                if (_userManager is null || _roleManager is null || _roleManager.Roles is null)
                {
                    throw new NullReferenceException(nameof(_userManager));
                }
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
            catch (NullReferenceException)
            {
                _busy = false;
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                _busy = false;
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(SaveUserAsync));
                _busy = false;
                throw;
            }
            finally
            {
                _busy = false;
            }
        }
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        public async Task<IEnumerable<ResponsibleUserDto>> LoadUserAsync(string roleName, CancellationToken token = default)
        {
            if (_busy) { return Enumerable.Empty<ResponsibleUserDto>(); }

            _busy = true;
            try
            {
                if (_roleManager is null || _roleManager.Roles is null)
                {
                    throw new NullReferenceException(nameof(_roleManager));
                }

                var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name.Contains(roleName.Trim()), token);
                if (role == null) throw new InvalidDataException(nameof(role));

                using var context = await _contextFactory.CreateDbContextAsync(token);
                if (context is null || context.Users is null || context.UserRoles is null || context.Roles is null)
                {
                    throw new NullReferenceException(nameof(context));
                }

                return await (from u in context.Users
                              join ur in context.UserRoles on u.Id equals ur.UserId into uur
                              from ur in uur.DefaultIfEmpty()
                              join r in context.Roles on ur.RoleId equals r.Id into rur
                              from r in rur.DefaultIfEmpty()
                              where r.Id.Contains(role.Id.Trim())
                              select new ResponsibleUserDto
                              {
                                  Id = u.Id,
                                  Name = u.UserName
                              }).ToListAsync(token);

            }
            catch (NullReferenceException)
            {
                _busy = false;
                throw;
            }
            catch (OperationCanceledException)
            {
                _busy = false;
                throw;
            }
            catch (Exception)
            {
                _logger.LogError("Something went wrong on {Method}", nameof(LoadUserAsync));
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
