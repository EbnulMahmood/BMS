using BMS.CoreBusiness.Entities;
using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.PluginIRepositories;

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
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }

    public sealed class ProjectService : IProjectService
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly IProjectRepository _repository;
        private readonly ICommonService _commonService;

        public ProjectService(IProjectRepository repository
            , ICommonService commonService)
        {
            _repository = repository;
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

                await _repository.SaveProjectAsync(project, token);
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
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
