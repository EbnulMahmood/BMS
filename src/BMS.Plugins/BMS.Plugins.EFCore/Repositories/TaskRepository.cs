using BMS.CoreBusiness.Dtos;
using BMS.CoreBusiness.Entities;
using BMS.Plugins.EFCore.Data;
using BMS.UseCases.PluginIRepositories;
using Microsoft.EntityFrameworkCore;

namespace BMS.Plugins.EFCore.Repositories
{
    internal sealed class TaskRepository : ITaskRepository
    {
        #region Properties & Object Initialization
        private readonly IDbContextFactory<BMSDbContext> _contextFactory;
        private bool _busy;

        public TaskRepository(IDbContextFactory<BMSDbContext> contextFactory)
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

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        public async Task<(IEnumerable<DevTaskDto>, int)> LoadTaskAsync(CancellationToken token = default)
        {
            if (_busy) { return (Enumerable.Empty<DevTaskDto>(), 0); }

            _busy = true;
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync(token);
                if (context is null || context.Tasks is null) { return (Enumerable.Empty<DevTaskDto>(), 0); }
                //Expression<Func<DevTaskDto, bool>> deletePredicate;
                //Expression<Func<DevTaskDto, bool>> conditionPredicate;

                int recordsCount = await context.Tasks
                //.Where(deletePredicate)
                //.Where(conditionPredicate)
                .CountAsync(cancellationToken: token);

                var taskList = await (from dt in context.Tasks
                                      join p in context.Projects on dt.ProjectId equals p.Id into dtp
                                      from p in dtp.DefaultIfEmpty()
                                      join r1 in context.Users on dt.Responsible1Id equals r1.Id into dtr1
                                      from r1 in dtr1.DefaultIfEmpty()
                                      join r2 in context.Users on dt.Responsible2Id equals r2.Id into dtr2
                                      from r2 in dtr2.DefaultIfEmpty()
                                      join cb in context.Users on dt.CreatedById.ToString() equals cb.Id into dtcb
                                      from cb in dtcb.DefaultIfEmpty()
                                      join lmb in context.Users on dt.LastModifiedById.ToString() equals lmb.Id into dtlmb
                                      from lmb in dtlmb.DefaultIfEmpty()
                                      join qar in context.Users on dt.QAResponsibleId equals qar.Id into dtqar
                                      from qar in dtqar.DefaultIfEmpty()
                                      orderby dt.TaskCreationDate descending
                                      select new DevTaskDto
                                      {
                                          Title = dt.Title,
                                          Status = dt.Status,
                                          Priority = dt.Priority,
                                          Project = p.Name,
                                          UXDesignLink = dt.UXDesignLink,
                                          Group = dt.Group,
                                          Responsible1 = r1.UserName,
                                          Responsible2 = r2.UserName,
                                          Release = dt.Release,
                                          CreatedBy = cb.UserName,
                                          LastModifiedBy = lmb.UserName,
                                          EstimatedHours = dt.EstimatedHours,
                                          ActualHours = dt.ActualHours,
                                          FRSMenuLink = dt.FRSMenuLink,
                                          UrlOrMenuOrWorkflow = dt.UrlOrMenuOrWorkflow,
                                          Remarks = dt.Remarks,
                                          TaskCompletedTime = dt.TaskCompletedTime,
                                          TaskCreationDate = dt.TaskCreationDate,
                                          QAResponsible = qar.UserName,
                                          QADoneTime = dt.QADoneTime,
                                          Review = dt.Review,
                                          ReviewRemarks = dt.ReviewRemarks,
                                          TestCaseFunctional = dt.TestCaseFunctional,
                                          TestFeatureAndScenario = dt.TestFeatureAndScenario,
                                          WebRequestKey = dt.WebRequestKey
                                      })
                               //.Where(deletePredicate)
                               //.Where(conditionPredicate)
                               .ToListAsync(token);

                return (taskList, recordsCount);
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
