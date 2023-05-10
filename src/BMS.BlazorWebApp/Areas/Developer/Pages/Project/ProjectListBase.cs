using BMS.BlazorWebApp.Areas.Developer.Pages.DevTask;
using BMS.BlazorWebApp.Settings;
using BMS.CoreBusiness.Dtos;
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
        public IProjectService ProjectService { get; private set; }
        protected IEnumerable<ProjectDto> Projects { get; private set; } = new List<ProjectDto>();
        protected PaginationState pagination = new() { ItemsPerPage = Constants.InitItemsPerPage };

        protected string nameFilter = string.Empty;
        protected IQueryable<ProjectDto>? FilteredItems => Projects?
            .AsQueryable()?
            .Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));

        protected bool IsLoading { get; private set; } = false;
        protected string Message { get; private set; } = string.Empty;
        protected string MessageType { get; private set; } = string.Empty;
        protected bool IsDisplayAddProject { get; private set; } = true;
        protected bool IsDisplayAleart { get; private set; } = false;

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
        protected Task EditAsync(ProjectDto projectDto)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Single Instances Loading Function
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
        #endregion

        #region Helper Function
        public void ToggleAddProject()
        {
            IsDisplayAddProject = false;
        }

        public async Task OnProjectAddCompletedAsync(string message, string type)
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
                Logger.LogError(ex, "Something went wrong on {Method}", nameof(OnProjectAddCompletedAsync));
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void DismissAleart()
        {
            IsDisplayAleart = false;
        }
        #endregion
    }
}
