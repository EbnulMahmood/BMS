using BMS.BlazorWebApp.Areas.Developer.Pages.DevTask;
using BMS.CoreBusiness.ViewModels.Membership;
using BMS.UseCases.Services.Membership;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BMS.BlazorWebApp.Areas.Administrator.Pages.Admin
{
    public class ManageBase : ComponentBase
    {
        #region Logger
        [Inject]
        protected ILogger<AddDevTaskBase> Logger { get; private set; }
        #endregion

        #region Properties & Object Initialization
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
            try
            {
                editContext = new EditContext(NewUser);

                RoleList = RoleManagerService.LoadRole();

                editContext.OnFieldChanged += (x, y) =>
                {
                    isInvalidForm = !editContext.Validate();
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Initialization {Method}", nameof(OnInitialized));
            }
        }
        #endregion

        #region Operational Function
        public async Task HandleSubmitAsync()
        {
            try
            {
                await UserManagerService.SaveUserAsync(NewUser);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Submit {Method}", nameof(HandleSubmitAsync));
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
