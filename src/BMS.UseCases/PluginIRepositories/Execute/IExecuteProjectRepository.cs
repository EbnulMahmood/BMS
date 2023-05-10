using BMS.CoreBusiness.Entities;

namespace BMS.UseCases.PluginIRepositories.Execute
{
    public interface IExecuteProjectRepository
    {
        #region Operational Function
        Task SaveProjectAsync(Project project, CancellationToken token = default);
        Task UpdateProjectAsync(Project project, CancellationToken token = default);
        #endregion
        #region Others Function
        #endregion
        #region Helper Function
        #endregion
    }
}
