using BMS.CoreBusiness.Dtos;

namespace BMS.UseCases.Tasks.Interfaces
{
    public interface ILoadTaskUseCase
    {
        Task<(IEnumerable<DevTaskDto>, int)> ExecuteAsync();
    }
}
