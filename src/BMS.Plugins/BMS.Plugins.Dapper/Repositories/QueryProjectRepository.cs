using BMS.CoreBusiness.Dtos;
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
        #endregion

        #region List Loading Function
        public async Task<IEnumerable<ProjectDto>> LoadProjectAsync(CancellationToken token = default)
        {
            if (_busy) { return Enumerable.Empty<ProjectDto>(); }

            _busy = true;
            try
            {
                string query = $@"/*QueryProjectRepository=>LoadProjectAsync*/
WITH project_ids AS (
  SELECT Id
  FROM Projects
)
SELECT
p.Name
,cb.UserName AS CreatedBy
,lmb.UserName AS LastModifiedBy
,p.CreatedOnUtc
,p.LastModifiedOnUtc
FROM project_ids AS tp
INNER JOIN Projects AS p ON tp.Id = p.Id
INNER JOIN AspNetUsers AS cb ON cb.Id = p.CreatedById
INNER JOIN AspNetUsers AS lmb ON lmb.Id = p.LastModifiedById
ORDER BY p.CreatedOnUtc DESC
";
                return await _context.LoadDataAsync<ProjectDto, dynamic>(query, new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadProjectAsync));
                _busy = false;
                throw;
            }
            finally
            {
                _busy = false;
            }
        }

        public async Task<IEnumerable<ProjectDropdownDto>> LoadProjectDropdownAsync(CancellationToken token = default)
        {
            if (_busy) { return Enumerable.Empty<ProjectDropdownDto>(); }

            _busy = true;
            try
            {
                string query = $@"/*QueryProjectRepository=>LoadProjectDropdownAsync*/
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
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadProjectDropdownAsync));
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
        public Task<bool> IsDuplicateProjectNameAsync(string name, CancellationToken token = default)
        {
            if (_busy) return Task.FromResult(false);

            _busy = true;
            try
            {
                string query = $@"/*QueryProjectRepository=>IsDuplicateProjectNameAsync*/
SELECT 
COUNT(*) 
FROM Projects WITH (INDEX(Index_Name))
WHERE Name = @Name
";
                return _context.GetFirstOrDefaultDataAsync<bool, dynamic>(query, new { Name = name.Trim() });
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
