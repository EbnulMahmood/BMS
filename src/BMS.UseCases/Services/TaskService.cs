using BMS.CoreBusiness.Dtos;
using BMS.UseCases.PluginIRepositories;

namespace BMS.UseCases.Services
{
    public interface ITaskService
    {
        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        Task<(IEnumerable<DevTaskDto>, int)> LoadTaskAsync(CancellationToken token = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }

    public class TaskService : ITaskService
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        public async Task<(IEnumerable<DevTaskDto>, int)> LoadTaskAsync(CancellationToken token = default)
        {
            try
            {
                (var taskList, int recordsCount) = await _repository.LoadTaskAsync(token);

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
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
