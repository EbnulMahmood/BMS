using BMS.CoreBusiness.Entities;
using BMS.Plugins.EFCore.Data;
using BMS.UseCases.PluginIRepositories;

namespace BMS.Plugins.EFCore.Repositories
{
    public sealed class ProjectRepository : IProjectRepository
    {
        #region Properties & Object Initialization
        private readonly BMSDbContext _context;

        public ProjectRepository(BMSDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Operational Function
        public async Task SaveProjectAsync(Project project, CancellationToken token = default)
        {
            try
            {
                await _context.Projects.AddAsync(project, token);
                await _context.SaveChangesAsync();
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
