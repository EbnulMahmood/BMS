using BMS.CoreBusiness.Dtos;
using BMS.UseCases.PluginIRepositories;
using BMS.UseCases.Tasks.Interfaces;

namespace BMS.UseCases.Tasks
{
    public sealed class LoadTaskUseCase : ILoadTaskUseCase
    {
        private readonly ITaskRepository _repository;

        public LoadTaskUseCase(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<DevTaskDto>, int)> ExecuteAsync()
        {
            try
            {
                (var taskList, int recordsCount) = await _repository.LoadTaskAsync();

                var taskDtoList = from dt in taskList
                                  select new DevTaskDto
                                  {
                                      Title = dt.Title,
                                      Status = dt.Status,
                                      Priority = dt.Priority,
                                      Project = dt.Project,
                                      UXDesignLink = dt.UXDesignLink,
                                      Group = dt.Group,
                                      EntryBy = dt.EntryBy,
                                      Responsible1 = dt.Responsible1,
                                      Responsible2 = dt.Responsible2,
                                      Release = dt.Release,
                                      EstimatedHours = dt.EstimatedHours,
                                      ActualHours = dt.ActualHours,
                                      FRSMenuLink = dt.FRSMenuLink,
                                      UrlOrMenuOrWorkflow = dt.UrlOrMenuOrWorkflow,
                                      Remarks = dt.Remarks,
                                      TaskCompletedTime = dt.TaskCompletedTime,
                                      TaskCreationDate = dt.TaskCreationDate,
                                      QAResponsible = dt.QAResponsible,
                                      QADoneTime = dt.QADoneTime,
                                      Review = dt.Review,
                                      ReviewRemarks = dt.ReviewRemarks,
                                      TestCaseFunctional = dt.TestCaseFunctional,
                                      TestFeatureAndScenario = dt.TestFeatureAndScenario,
                                      WebRequestKey = dt.WebRequestKey
                                  };

                return (taskDtoList, recordsCount);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
