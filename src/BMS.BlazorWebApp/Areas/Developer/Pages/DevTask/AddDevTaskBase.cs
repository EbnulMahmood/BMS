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
        #endregion

        #region Properties & Object Initialization
        protected DevTaskViewModelCreate ViewModelCreate { get; private set; }
        public IEnumerable<ProjectDto> ProjectDtoList { get; private set; } = Enumerable.Empty<ProjectDto>();
        public IEnumerable<ResponsibleDeveloperDto> ResponsibleDeveloperDtoList { get; set; } = Enumerable.Empty<ResponsibleDeveloperDto>();
        protected EditContext editContext;
        protected bool isInvalidForm = true;
        private readonly string _tasksUrl = "/Tasks";

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
            ViewModelCreate = new();
            editContext = new EditContext(ViewModelCreate);

            ProjectDtoList = await ProjectService.LoadProjectAsync();
            ResponsibleDeveloperDtoList = await UserManagerService.LoadUserAsync(RoleConstants.Developer);

            editContext.OnFieldChanged += (x, y) =>
            {
                isInvalidForm = !editContext.Validate();
            };
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
        protected void NavigateToTasks()
        {
            NavigationManager.NavigateTo(_tasksUrl);
        }
        #endregion
    }
}
