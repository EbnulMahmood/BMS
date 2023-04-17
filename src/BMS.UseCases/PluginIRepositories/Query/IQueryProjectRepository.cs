using BMS.CoreBusiness.Dtos;

namespace BMS.UseCases.PluginIRepositories.Query
{
    public interface IQueryProjectRepository
    {
        Task<IEnumerable<ProjectDto>> LoadProjectAsync(CancellationToken token = default);
    }
}
