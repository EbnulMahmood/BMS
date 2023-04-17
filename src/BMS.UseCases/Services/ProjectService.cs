using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.PluginIRepositories.Execute;
using BMS.UseCases.PluginIRepositories.Query;

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
        #endregion

        #region Properties & Object Initialization
        private readonly IExecuteProjectRepository _executeProjectRepository;
        private readonly IQueryProjectRepository _queryProjectRepository;
        private readonly ICommonService _commonService;

        public ProjectService(IExecuteProjectRepository executeProjectRepository
            , IQueryProjectRepository queryProjectRepository
            , ICommonService commonService)
        {
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
                if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));

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
            catch (Exception)
            {

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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProjectDropdownDto>> LoadProjectDropdownAsync(CancellationToken token = default)
        {
            try
            {
                return await _queryProjectRepository.LoadProjectDropdownAsync(token);
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
