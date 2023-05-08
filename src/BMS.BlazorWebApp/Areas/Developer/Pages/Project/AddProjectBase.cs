using BMS.BlazorWebApp.Areas.Developer.Pages.DevTask;
using BMS.BlazorWebApp.Helpers;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BMS.BlazorWebApp.Areas.Developer.Pages.Project
{
    public class AddProjectBase : ComponentBase
    {
        #region Logger
        [Inject]
        protected ILogger<AddDevTaskBase> Logger { get; private set; }
        #endregion

        #region Properties & Object Initialization
        public ProjectViewModelCreate ViewModelCreate { get; set; } = new();
        protected EditContext editContext;
        protected bool isInvalidForm = true;
        [Parameter]
        public EventCallback<(string, string)> OnProjectAddCompletedAsync { get; set; }

        [Inject]
        public IProjectService ProjectService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                editContext = new EditContext(ViewModelCreate);

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
        protected async Task HandleSubmitAsync()
        {
            string message = string.Empty;
            string type = "danger";
            try
            {
                await ProjectService.SaveProjectAsync(ViewModelCreate);
                message = $"{ViewModelCreate.Name} added successfully";
                type = "success";
            }
            catch (OperationCanceledException ex)
            {
                message = ex.Message;
            }
            catch (NullReferenceException ex)
            {
                message = ex.Message;
            }
            catch (InvalidDataException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {
                message = WebHelper.SetExceptionMessage(ex);
                Logger.LogError(ex, "Something went wrong on Submit {Method}", nameof(HandleSubmitAsync));
            }
            await OnProjectAddCompletedAsync.InvokeAsync((message, type));
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
