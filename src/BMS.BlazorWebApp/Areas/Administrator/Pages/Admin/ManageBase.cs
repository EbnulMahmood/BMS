using BMS.CoreBusiness.Entities.Membership;
using BMS.CoreBusiness.ViewModels.Membership;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BMS.BlazorWebApp.Areas.Administrator.Pages.Admin
{
    public class ManageBase : ComponentBase
    {
        [Inject]
        public UserManager<ApplicationUser> UserManager { get; private set; }
        [Inject]
        public SignInManager<ApplicationUser> SignInManager { get; private set; }
        protected EditContext editContext;
        protected bool isInvalidForm = true;

        public UserViewModel NewUser { get; protected set; } = new();
        public IEnumerable<RoleViewModel> RoleList { get; private set; } = Enumerable.Empty<RoleViewModel>();
        protected override void OnInitialized()
        {
            NewUser = new();
            editContext = new EditContext(NewUser);

            RoleList = new List<RoleViewModel>()
            {
                new RoleViewModel(){ Id = "e6797dad-1eab-4e83-bfda-f127e7ae12c9", RoleName = "Admin" },
                new RoleViewModel(){ Id = "92ae7bb4-1162-44e9-aa3a-21b0c6d6895f", RoleName = "Developer" },
                new RoleViewModel(){ Id = "57bb88e9-761d-4aca-8542-9d463e48e055", RoleName = "QA" }
            };

            editContext.OnFieldChanged += (x, y) =>
            {
                isInvalidForm = !editContext.Validate();
            };
        }

        public async Task HandleSubmitAsync()
        {
            try
            {
                string message = "Something Went Wrong!";

                var applicationUser = new ApplicationUser
                {
                    UserName = NewUser.LoginName,
                    Email = NewUser.LoginName,
                    EmailConfirmed = true
                };

                var result = await UserManager.CreateAsync(applicationUser, "@E0b0nul");

                if (result.Succeeded)
                {
                    message = "New User Added!";
                    var role = RoleList.FirstOrDefault(x => x.Id == NewUser.UserRole);
                    var roleAddedResult = await UserManager.AddToRoleAsync(applicationUser, role?.RoleName ?? "User");
                    if (roleAddedResult.Succeeded) { message = "New User and Role Added!"; }

                    var claimAddedResult = await UserManager.AddClaimAsync(applicationUser, new Claim(role?.RoleName ?? "User", role?.Id ?? ""));
                    if (claimAddedResult.Succeeded) { message = "New User, Role and Claim Added!"; }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
