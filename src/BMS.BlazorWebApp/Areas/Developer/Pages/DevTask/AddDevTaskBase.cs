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
        protected ILogger<AddDevTaskBase> Logger { get; private set; }
        #endregion

        #region Properties & Object Initialization
        protected DevTaskViewModelCreate ViewModelCreate { get; private set; } = new();
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
            try
            {
                editContext = new EditContext(ViewModelCreate);

                ProjectDropdownDtoList = await ProjectService.LoadProjectDtoDropdownAsync();
                ResponsibleDeveloperDtoList = await UserManagerService.LoadUserAsync(RoleConstants.Developer);
                ResponsibleQADtoList = await UserManagerService.LoadUserAsync(RoleConstants.QA);

                editContext.OnFieldChanged += (x, y) =>
                {
                    isInvalidForm = !editContext.Validate();
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Initialization {Method}", nameof(OnInitializedAsync));
            }
        }
        #endregion

        #region Operational Function
        protected async Task HandleSubmitAsync()
        {
            try
            {
                await TaskService.SaveTaskAsync(ViewModelCreate);
                NavigateToTasks();
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
        protected void NavigateToTasks()
        {
            try
            {
                NavigationManager.NavigateTo(_tasksUrl);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Navigation {Method}", nameof(NavigateToTasks));
            }
        }
        #endregion
    }
}
