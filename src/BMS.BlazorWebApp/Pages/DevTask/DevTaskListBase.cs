using BMS.CoreBusiness.Dtos;
using BMS.UseCases.Services;
using Microsoft.AspNetCore.Components;

namespace BMS.BlazorWebApp.Pages.Tasks
{
    public class DevTaskListBase : ComponentBase
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        public IEnumerable<DevTaskDto> Tasks { get; set; } = Enumerable.Empty<DevTaskDto>();
        public int recordsCount = 0;

        [Inject]
        public ITaskService TaskService { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadTaskAsync();
        }
        #endregion

        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        private async Task LoadTaskAsync()
        {
            (Tasks, recordsCount) = await TaskService.LoadTaskAsync();
        }
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
