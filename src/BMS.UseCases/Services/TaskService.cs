using BMS.CoreBusiness.BusinessRules;
using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities;
using BMS.CoreBusiness.Enums;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.PluginIRepositories.Execute;
using BMS.UseCases.PluginIRepositories.Query;
using Microsoft.Extensions.Logging;

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

    internal sealed class TaskService : ITaskService
    {
        #region Logger
        private readonly ILogger<TaskService> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly IExecuteTaskRepository _executeTaskRepository;
        private readonly IQueryTaskRepository _queryTaskRepository;
        private readonly ICommonService _commonService;

        public TaskService(IExecuteTaskRepository executeTaskRepository
            , IQueryTaskRepository queryTaskRepository
            , ICommonService commonService
            , ILogger<TaskService> logger)
        {
            _executeTaskRepository = executeTaskRepository;
            _queryTaskRepository = queryTaskRepository;
            _commonService = commonService;
            _logger = logger;
        }
        #endregion

        #region Operational Function
        public async Task SaveTaskAsync(DevTaskViewModelCreate viewModel, CancellationToken token = default)
        {
            try
            {
                if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));

                DevTaskViewModelValidation(viewModel);

                var devTask = new DevTask
                {
                    Title = viewModel.Title?.Trim(),
                    Status = DevTaskStatus.Pending,
                    Priority = viewModel.Priority,
                    ProjectId = viewModel.ProjectId,
                    UXDesignLink = viewModel.UXDesignLink?.Trim(),
                    Group = viewModel.Group?.Trim(),
                    Responsible1Id = viewModel.Responsible1Id,
                    Responsible2Id = viewModel.Responsible2Id,
                    Release = viewModel.Release,
                    FRSMenuLink = viewModel.FRSMenuLink?.Trim(),
                    UrlOrMenuOrWorkflow = viewModel.UrlOrMenuOrWorkflow?.Trim(),
                    Remarks = viewModel.Remarks?.Trim(),
                    TaskCreationDate = DateTimeOffset.UtcNow,
                    QAResponsibleId = viewModel.QAResponsibleId,
                    TestCaseFunctional = viewModel.TestCaseFunctional?.Trim(),
                    TestFeatureAndScenario = viewModel.TestFeatureAndScenario?.Trim(),
                    WebRequestKey = viewModel.WebRequestKey?.Trim(),
                    CreatedById = _commonService.GetCurrentUserId(),
                    CreatedOnUtc = DateTimeOffset.UtcNow,
                    LastModifiedById = _commonService.GetCurrentUserId(),
                    LastModifiedOnUtc = DateTimeOffset.UtcNow,
                    IPAddress = _commonService.RemoteIpAddress,
                };

                await _executeTaskRepository.SaveTaskAsync(devTask, token);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(SaveTaskAsync));
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
                return await _queryTaskRepository.LoadTaskAsync(token);
            }
            catch (Exception)
            {
                _logger.LogError("Something went wrong on {Method}", nameof(LoadTaskAsync));
                throw;
            }
        }
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        private static void DevTaskViewModelValidation(DevTaskViewModelCreate viewModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(viewModel.Title))
                {
                    throw new InvalidDataException("The Task field is required.");
                }
                else if (viewModel.Title.Length > ViewModelConstants.TaskSize)
                {
                    throw new InvalidDataException("Task is too long.");
                }

                if (viewModel.ProjectId.Equals(Guid.Empty))
                {
                    throw new InvalidDataException("The Project field is not valid.");
                }

                if (viewModel.Priority <= ViewModelConstants.PriorityMinSize)
                {
                    throw new InvalidDataException("The Priority field is not valid.");
                }

                if (viewModel.Release < ViewModelConstants.ReleaseMinSize)
                {
                    throw new InvalidDataException($"Release can't be less than {ViewModelConstants.ReleaseMinSize}.");
                }

                if (string.IsNullOrWhiteSpace(viewModel.Responsible1Id))
                {
                    throw new InvalidDataException("The Responsible 1 field is required.");
                }
            }
            catch (InvalidDataException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
