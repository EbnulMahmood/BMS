using BMS.BlazorWebApp.Helpers;
using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BMS.BlazorWebApp.Areas.Developer.Pages.Project
{
    public class EditProjectBase : ComponentBase
    {
        #region Logger
        [Inject]
        protected ILogger<EditProjectBase> Logger { get; private set; }
        #endregion

        #region Properties & Object Initialization
        public ProjectViewModelEdit ViewModelEdit { get; set; } = new();
        protected EditContext? editContext;
        protected bool isInvalidForm = true;
        [Parameter]
        public string ProjectId { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<(string, string)> OnProjectEditCompletedAsync { get; set; }

        [Inject]
        protected IProjectService ProjectService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await GetProjectDtoAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Initialization {Method}", nameof(OnInitialized));
            }
        }

        protected override void OnInitialized()
        {
            try
            {
                editContext = new EditContext(ViewModelEdit);

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
                await ProjectService.UpdateProjectAsync(ViewModelEdit);
                message = $"{ViewModelEdit.Name} updated successfully";
                type = "success";
            }
            catch (OperationCanceledException ex)
            {
                message = ex.Message;
                Logger.LogError(ex, ex.Message);
            }
            catch (NullReferenceException ex)
            {
                message = ex.Message;
                Logger.LogError(ex, ex.Message);
            }
            catch (InvalidDataException ex)
            {
                message = ex.Message;
                Logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                message = WebHelper.SetExceptionMessage(ex);
                Logger.LogError(ex, "Something went wrong on Submit {Method}", nameof(HandleSubmitAsync));
            }
            await OnProjectEditCompletedAsync.InvokeAsync((message, type));
        }
        #endregion

        #region Single Instances Loading Function
        public async Task GetProjectDtoAsync()
        {
            try
            {
                var projectDto = await ProjectService.GetProjectDtoByIdAsync(ProjectId);
                ViewModelEdit.Id = projectDto.Id;
                ViewModelEdit.Name = projectDto.Name;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on get {Method}", nameof(GetProjectDtoAsync));
            }
        }
        #endregion

        #region List Loading Function
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
