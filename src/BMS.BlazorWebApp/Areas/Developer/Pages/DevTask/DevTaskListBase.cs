using BMS.CoreBusiness.Dtos;
using BMS.UseCases.Services;
using Microsoft.AspNetCore.Components;

namespace BMS.BlazorWebApp.Areas.Developer.Pages.DevTask
{
    public class DevTaskListBase : ComponentBase
    {
        #region Logger
        [Inject]
        protected ILogger<AddDevTaskBase> Logger { get; private set; }
        #endregion

        #region Properties & Object Initialization
        public IEnumerable<DevTaskDto> Tasks { get; private set; } = Enumerable.Empty<DevTaskDto>();
        public int recordsCount = 0;

        [Inject]
        public ITaskService TaskService { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadTaskDtoAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong on Initialization {Method}", nameof(OnInitializedAsync));
            }
        }
        #endregion

        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        private async Task LoadTaskDtoAsync()
        {
            try
            {
                (Tasks, recordsCount) = await TaskService.LoadTaskDtoAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Something went wrong loading task {Method}", nameof(LoadTaskDtoAsync));
            }
        }
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
