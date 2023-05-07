﻿using BMS.BlazorWebApp.Areas.Developer.Pages.DevTask;
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
        protected IEnumerable<ProjectDto> Projects { get; private set; } = new List<ProjectDto>();
        protected PaginationState pagination = new() { ItemsPerPage = Constants.InitItemsPerPage };

        protected string nameFilter = string.Empty;
        protected IQueryable<ProjectDto>? FilteredItems => Projects?.AsQueryable()?.Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));

        protected string Message { get; private set; } = string.Empty;
        protected bool IsDisplayNone { get; private set; } = true;
        [Inject]
        public IProjectService ProjectService { get; private set; }
        public int recordsCount = 0;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadProjectAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Initialization {Method}", nameof(OnInitializedAsync));
            }
        }
        #endregion

        #region Operational Function
        protected Task EditAsync(ProjectDto projectDto)
        {
            Message = $"Editing {projectDto.Name}";
            return Task.CompletedTask;
        }
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        private async Task LoadProjectAsync()
        {
            try
            {
                Projects = await ProjectService.LoadProjectAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong loading task {Method}", nameof(LoadProjectAsync));
            }
        }
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        public void ToggleAddProduct()
        {
            IsDisplayNone = false;
        }

        public async Task OnProjectAddAsync()
        {
            await LoadProjectAsync();
        }
        #endregion
    }
}
