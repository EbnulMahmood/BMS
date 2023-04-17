using BMS.CoreBusiness.Dtos;
using BMS.Plugins.Dapper.Data;
using BMS.UseCases.PluginIRepositories.Query;

namespace BMS.Plugins.Dapper.Repositories
{
    internal sealed class QueryProjectRepository : IQueryProjectRepository
    {
        #region Properties & Object Initialization
        private readonly IRelationalDataAccess _context;
        private bool _busy;

        public QueryProjectRepository(IRelationalDataAccess context)
        {
            _context = context;
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
                string query = $@"/*ProjectRepository=>LoadProject*/
SELECT 
Id
,Name
,CreatedById
,CreatedOnUtc
,LastModifiedById
,LastModifiedOnUtc
,IPAddress
FROM Projects
";
                var projectDtoList = await _context.LoadDataAsync<ProjectDto, dynamic>(query, new { });

                return projectDtoList;
            }
            catch (Exception)
            {
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
