using BMS.CoreBusiness.Entities;

namespace BMS.UseCases.PluginIRepositories
{
    public interface IProjectRepository
    {
        #region Operational Function
        Task SaveProjectAsync(Project project, CancellationToken token = default);
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}