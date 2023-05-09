using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.PluginIRepositories.Execute;
using BMS.UseCases.PluginIRepositories.Query;
using Microsoft.Extensions.Logging;

namespace BMS.UseCases.Services
{
    public interface IProjectService
    {
        #region Operational Function
        Task SaveProjectAsync(ProjectViewModelCreate viewModel, CancellationToken token = default);
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        Task<IEnumerable<ProjectDto>> LoadProjectAsync(CancellationToken token = default);
        Task<IEnumerable<ProjectDropdownDto>> LoadProjectDropdownAsync(CancellationToken token = default);
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }

    internal sealed class ProjectService : IProjectService
    {
        #region Logger
        private readonly ILogger<ProjectService> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly IExecuteProjectRepository _executeProjectRepository;
        private readonly IQueryProjectRepository _queryProjectRepository;
        private readonly ICommonService _commonService;

        public ProjectService(IExecuteProjectRepository executeProjectRepository
            , IQueryProjectRepository queryProjectRepository
            , ICommonService commonService
            , ILogger<ProjectService> logger)
        {
            _logger = logger;
            _executeProjectRepository = executeProjectRepository;
            _queryProjectRepository = queryProjectRepository;
            _commonService = commonService;
        }
        #endregion

        #region Operational Function
        public async Task SaveProjectAsync(ProjectViewModelCreate viewModel, CancellationToken token = default)
        {
            try
            {
                if (token.IsCancellationRequested == true)
                {
                    throw new OperationCanceledException("Operation was canceled");
                }
                
                if (viewModel is null) throw new NullReferenceException("Null project found");

                await CkeckBeforeSaveAsync(viewModel.Name, token);

                var project = new Project
                {
                    Name = viewModel.Name?.Trim(),
                    CreatedById = _commonService.GetCurrentUserId(),
                    CreatedOnUtc = DateTimeOffset.UtcNow,
                    LastModifiedById = _commonService.GetCurrentUserId(),
                    LastModifiedOnUtc = DateTimeOffset.UtcNow,
                    IPAddress = _commonService.RemoteIpAddress,
                };

               await _executeProjectRepository.SaveProjectAsync(project, token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullReferenceException)
            {
                throw;
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
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(SaveProjectAsync));
                throw;
            }
        }
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        public async Task<IEnumerable<ProjectDto>> LoadProjectAsync(CancellationToken token = default)
        {
            try
            {
                return await _queryProjectRepository.LoadProjectAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadProjectAsync));
                throw;
            }
        }

        public async Task<IEnumerable<ProjectDropdownDto>> LoadProjectDropdownAsync(CancellationToken token = default)
        {
            try
            {
                return await _queryProjectRepository.LoadProjectDropdownAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadProjectDropdownAsync));
                throw;
            }
        }
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        private async Task CkeckBeforeSaveAsync(string name, CancellationToken token = default)
        {
            try
            {
                if (token.IsCancellationRequested == true)
                {
                    throw new OperationCanceledException("Operation was canceled");
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var isDuplicateProjectName = await _queryProjectRepository.IsDuplicateProjectNameAsync(name, token);

                if (isDuplicateProjectName == true)
                {
                    throw new InvalidDataException($"Project \"{name}\" already exists in database");
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
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
