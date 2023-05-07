using BMS.CoreBusiness.Dtos;

namespace BMS.UseCases.PluginIRepositories.Query
{
    public interface IQueryProjectRepository
    {
        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        Task<IEnumerable<ProjectDto>> LoadProjectAsync(CancellationToken token = default);
        Task<IEnumerable<ProjectDropdownDto>> LoadProjectDropdownAsync(CancellationToken token = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        Task<bool> IsDuplicateProjectNameAsync(string name, CancellationToken token = default);
        #endregion
    }
}
