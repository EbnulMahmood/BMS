using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.PluginIRepositories;

namespace BMS.UseCases.Services
{
    public interface ITaskService
    {
        #region Operational Function
        Task SaveTaskAsync(DevTaskViewModelCreate viewModel, CancellationToken token = default);
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

    public sealed class TaskService : ITaskService
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
        public async Task SaveTaskAsync(DevTaskViewModelCreate viewModel, CancellationToken token = default)
        {
            try
            {
                if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

                var devTask = new DevTask
                {
                    Title = viewModel.Title?.Trim(),
                    Status = viewModel.Status,
                    Priority = viewModel.Priority,
                    Project = viewModel.Project?.Trim(),
                    UXDesignLink = viewModel.UXDesignLink?.Trim(),
                    Group = viewModel.Group?.Trim(),
                    EntryBy = viewModel.EntryBy?.Trim(),
                    Responsible1 = viewModel.Responsible1?.Trim(),
                    Responsible2 = viewModel.Responsible2?.Trim(),
                    Release = viewModel.Release,
                    EstimatedHours = viewModel.EstimatedHours,
                    ActualHours = viewModel.ActualHours,
                    FRSMenuLink = viewModel.FRSMenuLink?.Trim(),
                    UrlOrMenuOrWorkflow = viewModel.UrlOrMenuOrWorkflow?.Trim(),
                    Remarks = viewModel.Remarks?.Trim(),
                    TaskCompletedTime = viewModel.TaskCompletedTime,
                    TaskCreationDate = viewModel.TaskCreationDate,
                    QAResponsible = viewModel.QAResponsible?.Trim(),
                    QADoneTime = viewModel.QADoneTime,
                    Review = viewModel.Review?.Trim(),
                    ReviewRemarks = viewModel.ReviewRemarks?.Trim(),
                    TestCaseFunctional = viewModel.TestCaseFunctional?.Trim(),
                    TestFeatureAndScenario = viewModel.TestFeatureAndScenario?.Trim(),
                    WebRequestKey = viewModel.WebRequestKey?.Trim(),
                };

                await _repository.SaveTaskAsync(devTask, token);
            }
            catch (Exception)
            {

                throw;
            }
        }
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
