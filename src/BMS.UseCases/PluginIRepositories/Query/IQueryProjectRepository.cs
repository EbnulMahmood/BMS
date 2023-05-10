using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities;

namespace BMS.UseCases.PluginIRepositories.Query
{
    public interface IQueryProjectRepository
    {
        #region Single Instances Loading Function
        Task<ProjectDto> GetProjectDtoByIdAsync(string id, CancellationToken token = default);
        Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken token = default);
        #endregion

        #region List Loading Function
        Task<IEnumerable<ProjectDto>> LoadProjectDtoAsync(CancellationToken token = default);
        Task<IEnumerable<ProjectDropdownDto>> LoadProjectDtoDropdownAsync(CancellationToken token = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        Task<bool> IsDuplicateProjectNameAsync(string name, Guid projectId = default, CancellationToken token = default);
        #endregion
    }
}
