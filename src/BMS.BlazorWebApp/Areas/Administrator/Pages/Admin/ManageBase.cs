using BMS.CoreBusiness.ViewModels.Membership;
using BMS.UseCases.Services.Membership;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BMS.BlazorWebApp.Areas.Administrator.Pages.Admin
{
    public class ManageBase : ComponentBase
    {
        [Inject]
        public IUserManagerService UserManagerService { get; private set; }
        [Inject]
        public IRoleManagerService RoleManagerService { get; set; }
        protected EditContext editContext;
        protected bool isInvalidForm = true;

        public UserViewModel NewUser { get; protected set; } = new();
        public IEnumerable<RoleViewModel> RoleList { get; set; } = new List<RoleViewModel>();
        protected override void OnInitialized()
        {
            NewUser = new();
            editContext = new EditContext(NewUser);

            RoleList = RoleManagerService.LoadRole();

            editContext.OnFieldChanged += (x, y) =>
            {
                isInvalidForm = !editContext.Validate();
            };
        }

        public async Task HandleSubmitAsync()
        {
            try
            {
                await UserManagerService.SaveUserAsync(NewUser);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
