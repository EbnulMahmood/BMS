using BMS.CoreBusiness.Dtos;

namespace BMS.UseCases.PluginIRepositories.Query
{
    public interface IQueryTaskRepository
    {
        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        Task<(IEnumerable<DevTaskDto>, int)> LoadTaskDtoAsync(CancellationToken token = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
