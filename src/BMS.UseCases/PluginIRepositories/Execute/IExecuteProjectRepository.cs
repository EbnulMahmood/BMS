using BMS.CoreBusiness.Entities;

namespace BMS.UseCases.PluginIRepositories.Execute
{
    public interface IExecuteProjectRepository
    {
        Task SaveProjectAsync(Project project, CancellationToken token = default);
    }
}
