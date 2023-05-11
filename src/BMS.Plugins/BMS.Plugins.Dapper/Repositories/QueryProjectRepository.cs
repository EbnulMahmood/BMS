using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities;
using BMS.Plugins.Dapper.Data;
using BMS.UseCases.PluginIRepositories.Query;
using Microsoft.Extensions.Logging;

namespace BMS.Plugins.Dapper.Repositories
{
    internal sealed class QueryProjectRepository : IQueryProjectRepository
    {
        #region Logger
        private readonly ILogger<QueryTaskRepository> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly IRelationalDataAccess _context;
        private bool _busy;

        public QueryProjectRepository(IRelationalDataAccess context
        , ILogger<QueryTaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Single Instances Loading Function
        public async Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken token = default)
        {
            if (_busy) { return default; }

            _busy = true;
            try
            {
                string query = $@"/*QueryProjectRepository=>GetProjectByIdAsync*/
SELECT 
*
FROM Projects
WHERE Id = @Id
";
                return await _context.GetFirstOrDefaultDataAsync<Project, dynamic>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(GetProjectByIdAsync));
                _busy = false;
                throw;
            }
            finally
            {
                _busy = false;
            }
        }

        public async Task<ProjectDto> GetProjectDtoByIdAsync(string id, CancellationToken token = default)
        {
            if (_busy) { return default; }

            _busy = true;
            try
            {
                string query = $@"/*QueryProjectRepository=>GetProjectDtoByIdAsync*/
SELECT
p.Id
,p.Name
,cb.UserName AS CreatedBy
,lmb.UserName AS LastModifiedBy
,p.CreatedOnUtc
,p.LastModifiedOnUtc
FROM Projects AS p
INNER JOIN AspNetUsers AS cb ON cb.Id = p.CreatedById
INNER JOIN AspNetUsers AS lmb ON lmb.Id = p.LastModifiedById
WHERE p.Id = @Id
";
                return await _context.GetFirstOrDefaultDataAsync<ProjectDto, dynamic>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(GetProjectDtoByIdAsync));
                _busy = false;
                throw;
            }
            finally
            {
                _busy = false;
            }
        }
        #endregion

        #region List Loading Function
        public async Task<IEnumerable<ProjectDto>> LoadProjectDtoAsync(CancellationToken token = default)
        {
            if (_busy) { return Enumerable.Empty<ProjectDto>(); }

            _busy = true;
            try
            {
                string query = $@"/*QueryProjectRepository=>LoadProjectDtoAsync*/
SELECT
p.Id
,p.Name
,cb.UserName AS CreatedBy
,lmb.UserName AS LastModifiedBy
,p.CreatedOnUtc
,p.LastModifiedOnUtc
FROM Projects AS p
INNER JOIN AspNetUsers AS cb ON cb.Id = p.CreatedById
INNER JOIN AspNetUsers AS lmb ON lmb.Id = p.LastModifiedById
ORDER BY p.CreatedOnUtc DESC
";
                return await _context.LoadDataAsync<ProjectDto, dynamic>(query, new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadProjectDtoAsync));
                _busy = false;
                throw;
            }
            finally
            {
                _busy = false;
            }
        }

        public async Task<IEnumerable<ProjectDropdownDto>> LoadProjectDtoDropdownAsync(CancellationToken token = default)
        {
            if (_busy) { return Enumerable.Empty<ProjectDropdownDto>(); }

            _busy = true;
            try
            {
                string query = $@"/*QueryProjectRepository=>LoadProjectDtoDropdownAsync*/
SELECT 
Id
,Name
FROM Projects
";
                var projectDtoList = await _context.LoadDataAsync<ProjectDropdownDto, dynamic>(query, new { });

                return projectDtoList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadProjectDtoDropdownAsync));
                _busy = false;
                throw;
            }
            finally
            {
                _busy = false;
            }
        }
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        public Task<bool> IsDuplicateProjectNameAsync(string name, Guid projectId = default, CancellationToken token = default)
        {
            if (_busy) return Task.FromResult(false);

            _busy = true;
            try
            {
                string conditionQuery = string.Empty;
                if (projectId != default && projectId != Guid.Empty)
                {
                    conditionQuery = $@"AND Id != @Id";
                }

                string query = $@"/*QueryProjectRepository=>IsDuplicateProjectNameAsync*/
SELECT 
COUNT(*) 
FROM Projects WITH (INDEX(Index_Name))
WHERE Name = @Name
{conditionQuery}
";
                return _context.GetFirstOrDefaultDataAsync<bool, dynamic>(query, new { Name = name.Trim(), Id = projectId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(IsDuplicateProjectNameAsync));
                _busy = false;
                throw;
            }
            finally
            {
                _busy = false;
            }
        }
        #endregion
    }
}
