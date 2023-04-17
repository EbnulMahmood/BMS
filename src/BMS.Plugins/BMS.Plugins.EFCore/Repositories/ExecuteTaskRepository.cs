using BMS.CoreBusiness.Entities;
using BMS.Plugins.EFCore.Data;
using BMS.UseCases.PluginIRepositories.Execute;
using Microsoft.EntityFrameworkCore;

namespace BMS.Plugins.EFCore.Repositories
{
    internal class ExecuteTaskRepository : IExecuteTaskRepository
    {
        #region Properties & Object Initialization
        private readonly IDbContextFactory<BMSDbContext> _contextFactory;
        private bool _busy;

        public ExecuteTaskRepository(IDbContextFactory<BMSDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        #endregion

        #region Operational Function
        public async Task SaveTaskAsync(DevTask devTask, CancellationToken token = default)
        {
            if (_busy) { return; }

            _busy = true;
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync(token);
                if (context is null || context.Tasks is null) { return; }

                await context.Tasks.AddAsync(devTask, token);
                await context.SaveChangesAsync(token);
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
