using BMS.BlazorWebApp.Settings;
using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.Services;
using BMS.UseCases.Services.Membership;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BMS.BlazorWebApp.Areas.Developer.Pages.DevTask
{
    public class AddDevTaskBase : ComponentBase
    {
        #region Logger
        [Inject]
        protected Microsoft.Extensions.Logging.ILogger<AddDevTaskBase> Logger { get; private set; }
        #endregion

        #region Properties & Object Initialization
        protected DevTaskViewModelCreate ViewModelCreate { get; private set; }
        public IEnumerable<ProjectDropdownDto> ProjectDropdownDtoList { get; private set; } = Enumerable.Empty<ProjectDropdownDto>();
        public IEnumerable<ResponsibleUserDto> ResponsibleDeveloperDtoList { get; set; } = Enumerable.Empty<ResponsibleUserDto>();
        public IEnumerable<ResponsibleUserDto> ResponsibleQADtoList { get; set; } = Enumerable.Empty<ResponsibleUserDto>();
        protected EditContext editContext;
        protected bool isInvalidForm = true;
        private readonly string _tasksUrl = "/Developer/Tasks";

        [Inject]
        public ITaskService TaskService { get; private set; }
        [Inject]
        public IProjectService ProjectService { get; private set; }
        [Inject]
        public IUserManagerService UserManagerService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Logger.LogInformation("Initialize started");
            try
            {
                ViewModelCreate = new();
                editContext = new EditContext(ViewModelCreate);

                ProjectDropdownDtoList = await ProjectService.LoadProjectDropdownAsync();
                ResponsibleDeveloperDtoList = await UserManagerService.LoadUserAsync(RoleConstants.Developer);
                ResponsibleQADtoList = await UserManagerService.LoadUserAsync(RoleConstants.QA);

                editContext.OnFieldChanged += (x, y) =>
                {
                    isInvalidForm = !editContext.Validate();
                };
                Logger.LogInformation("Initialize completed");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Initialization");
            }
        }
        #endregion

        #region Operational Function
        protected async Task HandleSubmitAsync()
        {
            Logger.LogInformation("Initialize started");
            try
            {
                await TaskService.SaveTaskAsync(ViewModelCreate);
                Logger.LogInformation("Initialize started");
                NavigateToTasks();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Initialization");
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
        protected void NavigateToTasks()
        {
            try
            {
                Logger.LogInformation("Navigate To Tasks stated");
                NavigationManager.NavigateTo(_tasksUrl);
                Logger.LogInformation("Navigate To Tasks completed");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Navigation");
            }
        }
        #endregion
    }
}
