using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BMS.BlazorWebApp.Areas.Developer.Pages.DevTask
{
    public class AddDevTaskBase : ComponentBase
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        protected DevTaskViewModelCreate ViewModelCreate { get; set; }
        protected EditContext editContext;
        protected bool isInvalidForm = true;
        private readonly string _tasksUrl = "/Tasks";

        [Inject]
        public ITaskService TaskService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            ViewModelCreate = new();
            editContext = new EditContext(ViewModelCreate);

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
