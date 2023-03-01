using BMS.CoreBusiness.Entities;

namespace BMS.UseCases.PluginIRepositories
{
    public interface ITaskRepository
    {
        #region Operational Function
        Task SaveTaskAsync(DevTask devTask);
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        Task<(IEnumerable<DevTask>, int)> LoadTaskAsync(CancellationToken token = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
