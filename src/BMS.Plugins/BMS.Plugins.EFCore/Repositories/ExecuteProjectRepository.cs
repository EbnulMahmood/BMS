using BMS.CoreBusiness.Entities;
using BMS.Plugins.EFCore.Data;
using BMS.UseCases.PluginIRepositories.Execute;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BMS.Plugins.EFCore.Repositories
{
    internal sealed class ExecuteProjectRepository : IExecuteProjectRepository
    {
        #region Logger
        private readonly ILogger<ExecuteProjectRepository> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly IDbContextFactory<BMSDbContext> _contextFactory;
        private bool _busy;

        public ExecuteProjectRepository(IDbContextFactory<BMSDbContext> contextFactory, ILogger<ExecuteProjectRepository> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }
        #endregion

        #region Operational Function
        public async Task SaveProjectAsync(Project project, CancellationToken token = default)
        {
            if (_busy) { return; }

            _busy = true;
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync(token);
                if (context is null || context.Projects is null)
                {
                    throw new NullReferenceException(nameof(context));
                }

                await context.Projects.AddAsync(project, token);
                await context.SaveChangesAsync(token);
            }
            catch (NullReferenceException)
            {
                _busy = false;
                throw;
            }
            catch (OperationCanceledException)
            {
                _busy = false;
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {

                _busy = false;
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong on {Method}", nameof(SaveProjectAsync));
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
