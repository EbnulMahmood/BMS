using BMS.CoreBusiness.Entities;

namespace BMS.UseCases.PluginIRepositories.Execute
{
    public interface IExecuteTaskRepository
    {
        Task SaveTaskAsync(DevTask devTask, CancellationToken token = default);
    }
}
