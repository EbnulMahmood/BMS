using BMS.BlazorWebApp.Areas.Developer.Pages.DevTask;
using BMS.BlazorWebApp.Helpers;
using BMS.BlazorWebApp.Settings;
using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace BMS.BlazorWebApp.Areas.Developer.Pages.Project
{
    public class ProjectListBase : ComponentBase
    {
        #region Logger
        [Inject]
        protected ILogger<AddDevTaskBase> Logger { get; private set; }
        #endregion

        #region Properties & Object Initialization
        [Inject]
        protected IProjectService ProjectService { get; private set; }
        protected IEnumerable<ProjectDto> Projects { get; private set; } = new List<ProjectDto>();
        protected PaginationState pagination = new() { ItemsPerPage = Constants.InitItemsPerPage };

        protected string nameFilter = string.Empty;
        protected IQueryable<ProjectDto>? FilteredItems => Projects?
            .AsQueryable()?
            .Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));

        protected bool IsLoading { get; private set; } = false;
        protected bool IsDisplayClose { get; private set; } = false;
        protected string Message { get; private set; } = string.Empty;
        protected string MessageType { get; private set; } = string.Empty;
        protected bool DisplayAddButton { get; private set; } = true;
        protected bool IsEditing { get; private set; } = false;
        protected bool IsDetailsView { get; private set; } = false;
        protected bool IsDisplayAleart { get; private set; } = false;
        protected bool DialogIsOpen { get; private set; } = false;
        private Guid ProjectId { get; set; } = default;
        protected string ProjectDeleteMessage { get; set; } = string.Empty;
        protected ProjectViewModelDetils ProjectViewModelDetils { get; private set; } = new();
        protected ProjectViewModelEdit ProjectViewModelEdit { get; private set; } = new();

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            try
            {
                await LoadProjectDtoAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Initialization {Method}", nameof(OnInitializedAsync));
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

        #region Operational Function
        protected void Edit(ProjectDto projectDto)
        {
            IsLoading = true;
            try
            {
                IsDisplayClose = true;
                ProjectViewModelEdit = ProjectViewModelEdit with { Id = projectDto.Id, Name = projectDto.Name };
                UIConditionChange(displayAddButton: false, isEditing: true, isDetailsView: false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong edit {Method}", nameof(Edit));
            }
            finally
            {
                IsLoading = false;
            }
            StateHasChanged();
        }

        public async Task OnDialogCloseAsync(bool isOk)
        {
            if (isOk is true)
            {
                string message = string.Empty;
                string type = "danger";
                IsLoading = true;
                try
                {
                    await ProjectService.DeleteProjectAsync(ProjectId);
                    message = "Project deleted successfully";
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
                catch (ArgumentNullException ex)
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
                    Logger.LogError(ex, "Something went wrong OnDialogClose {Method}", nameof(OnDialogCloseAsync));
                }
                finally
                {
                    IsLoading = false;
                }
                IsDisplayAleart = true;
                await OnProjectActionCompletedAsync(message, type);
            }
            DialogIsOpen = false;
            DisplayAddButton = true;
            IsDisplayClose = false;
        }
        #endregion

        #region Single Instances Loading Function
        protected void Details(ProjectDto projectDto)
        {
            IsLoading = true;
            try
            {
                IsDisplayClose = true;
                ProjectViewModelDetils = ProjectViewModelDetils with { Name = projectDto.Name, CreatedBy = projectDto.CreatedBy, LastModifiedBy = projectDto.LastModifiedBy, CreatedOnUtc = projectDto.CreatedOnUtc, LastModifiedOnUtc = projectDto.LastModifiedOnUtc };
                UIConditionChange(displayAddButton: false, isEditing: false, isDetailsView: true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong details {Method}", nameof(Details));
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

        #region List Loading Function
        private async Task LoadProjectDtoAsync()
        {
            IsLoading = true;
            try
            {
                Projects = await ProjectService.LoadProjectDtoAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong loading task {Method}", nameof(LoadProjectDtoAsync));
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

        #region Others Function
        private void UIConditionChange(bool displayAddButton = false, bool isEditing = false, bool isDetailsView = false, bool isDisplayAleart = false)
        {
            DisplayAddButton = displayAddButton;
            IsEditing = isEditing;
            IsDetailsView = isDetailsView;
            IsDisplayAleart = isDisplayAleart;
        }
        #endregion

        #region Helper Function
        protected void ToggleAddProjectButton()
        {
            IsDisplayAleart = false;
            DisplayAddButton = false;
            IsDisplayClose = true;
        }

        protected async Task OnProjectActionCompletedAsync(string message, string type)
        {
            IsLoading = true;
            try
            {
                Message = message ?? string.Empty;
                MessageType = type ?? string.Empty;
                IsDisplayAleart = true;
                await LoadProjectDtoAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on {Method}", nameof(OnProjectActionCompletedAsync));
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected void DismissAleart()
        {
            IsDisplayAleart = false;
        }

        protected void CloseButton()
        {
            IsDisplayClose = false;
            IsEditing = false;
            IsDetailsView = false;
            DisplayAddButton = true;
            IsDisplayAleart = false;
        }

        public void OpenDialog(Guid id, string title)
        {
            DialogIsOpen = true;
            ProjectId = id;
            ProjectDeleteMessage = $"Are you sure, you want to delete {title}?";
        }
        #endregion
    }
}
