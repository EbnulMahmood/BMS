using BMS.BlazorWebApp.Areas.Developer.Pages.DevTask;
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
        protected bool IsColosed { get; private set; } = true;
        protected string Message { get; private set; } = string.Empty;
        protected string MessageType { get; private set; } = string.Empty;
        protected bool DisplayAddButton { get; private set; } = true;
        protected bool IsEditing { get; private set; } = false;
        protected bool IsDetailsView { get; private set; } = false;
        protected bool IsDisplayAleart { get; private set; } = false;
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
        #endregion

        #region Single Instances Loading Function
        protected void Details(ProjectDto projectDto)
        {
            IsLoading = true;
            try
            {
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
        private void UIConditionChange(bool displayAddButton = false, bool isEditing = false, bool isDetailsView = false)
        {
            DisplayAddButton = displayAddButton;
            IsEditing = isEditing;
            IsDetailsView = isDetailsView;
            IsDisplayAleart = false;
        }
        #endregion

        #region Helper Function
        protected void ToggleAddProjectButton()
        {
            DisplayAddButton = false;
        }

        protected async Task OnProjectAddOrUpdateCompletedAsync(string message, string type)
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
                Logger.LogError(ex, "Something went wrong on {Method}", nameof(OnProjectAddOrUpdateCompletedAsync));
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
            IsColosed = false;
        }  
        #endregion
    }
}
