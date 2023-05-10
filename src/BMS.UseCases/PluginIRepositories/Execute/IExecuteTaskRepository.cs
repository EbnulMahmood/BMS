using BMS.CoreBusiness.Entities;

namespace BMS.UseCases.PluginIRepositories.Execute
{
    public interface IExecuteTaskRepository
    {
        #region Operational Function
        Task SaveTaskAsync(DevTask devTask, CancellationToken token = default);
        #endregion
        #region Others Function
        #endregion
        #region Helper Function
        #endregion
    }
}
