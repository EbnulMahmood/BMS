using BMS.CoreBusiness.Dtos;
using BMS.Plugins.Dapper.Data;
using BMS.UseCases.PluginIRepositories.Query;
using Microsoft.Extensions.Logging;

namespace BMS.Plugins.Dapper.Repositories
{
    internal sealed class QueryTaskRepository : IQueryTaskRepository
    {
        #region Logger
        private readonly ILogger<QueryTaskRepository> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly IRelationalDataAccess _context;
        private bool _busy;

        public QueryTaskRepository(IRelationalDataAccess context
        , ILogger<QueryTaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        public async Task<(IEnumerable<DevTaskDto>, int)> LoadTaskDtoAsync(CancellationToken token = default)
        {
            if (_busy) { return (Enumerable.Empty<DevTaskDto>(), 0); }

            _busy = true;
            try
            {
                IEnumerable<DevTaskDto> taskList = new List<DevTaskDto>();

                string countQuery = $@"/*QueryTaskRepository=>LoadTaskDtoAsync*/
SELECT COUNT(*) FROM Tasks
";
                int recordsCount = await _context.GetFirstOrDefaultDataAsync<int, dynamic>(countQuery, new { });

                if (recordsCount > 0)
                {
                    string query = $@"/*QueryTaskRepository=>LoadTaskDtoAsync*/
SELECT
t.Title
,t.Status
,t.Priority
,p.Name AS Project
,t.UXDesignLink
,t.[Group]
,r1.UserName AS Responsible1
,r2.UserName AS Responsible2
,t.Release
,cb.UserName AS CreatedBy
,lmb.UserName AS LastModifiedBy
,t.EstimatedHours
,t.ActualHours
,t.FRSMenuLink
,t.UrlOrMenuOrWorkflow
,t.Remarks
,t.TaskCompletedTime
,t.TaskCreationDate
,qar.UserName AS QAResponsible
,t.QADoneTime
,t.Review
,t.ReviewRemarks
,t.TestCaseFunctional
,t.TestFeatureAndScenario
,t.WebRequestKey
FROM Tasks AS t
INNER JOIN Projects AS p ON p.Id = t.ProjectId
INNER JOIN AspNetUsers AS cb ON cb.Id = t.CreatedById
INNER JOIN AspNetUsers AS lmb ON lmb.Id = t.LastModifiedById
LEFT JOIN AspNetUsers AS r1 ON r1.Id = t.Responsible1Id
LEFT JOIN AspNetUsers AS r2 ON r2.Id = t.Responsible2Id
LEFT JOIN AspNetUsers AS qar ON qar.Id = t.QAResponsibleId
";
                    taskList = await _context.LoadDataAsync<DevTaskDto, dynamic>(query, new { });
                }

                return (taskList, recordsCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(LoadTaskDtoAsync));
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
        #endregion
    }
}
