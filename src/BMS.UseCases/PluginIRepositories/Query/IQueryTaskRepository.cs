using BMS.CoreBusiness.Dtos;

namespace BMS.UseCases.PluginIRepositories.Query
{
    public interface IQueryTaskRepository
    {
        Task<(IEnumerable<DevTaskDto>, int)> LoadTaskAsync(CancellationToken token = default);
    }
}
