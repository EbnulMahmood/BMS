using BMS.CoreBusiness.Entities;
using BMS.Plugins.EFCore.Data;
using BMS.UseCases.PluginIRepositories;
using Microsoft.EntityFrameworkCore;

namespace BMS.Plugins.EFCore.Repositories
{
    public sealed class TaskRepository : ITaskRepository
    {
        #region Properties & Object Initialization
        private readonly BMSDbContext _context;

        public TaskRepository(BMSDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Operational Function
        public async Task SaveTaskAsync(DevTask devTask, CancellationToken token = default)
        {
            try
            {
                await _context.Tasks.AddAsync(devTask, token);
                await _context.SaveChangesAsync(token);
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
        public async Task<(IEnumerable<DevTask>, int)> LoadTaskAsync(CancellationToken token = default)
        {
            try
            {
                //Expression<Func<DevTask, bool>> deletePredicate;
                //Expression<Func<DevTask, bool>> conditionPredicate;

                int recordsCount = await _context.Tasks
                //.Where(deletePredicate)
                //.Where(conditionPredicate)
                .CountAsync(cancellationToken: token);

                var taskList = await (from dt in _context.Tasks
                                      orderby dt.TaskCreationDate descending
                                      select new DevTask
                                      {
                                          Title = dt.Title,
                                          Status = dt.Status,
                                          Priority = dt.Priority,
                                          Project = dt.Project,
                                          UXDesignLink = dt.UXDesignLink,
                                          Group = dt.Group,
                                          EntryBy = dt.EntryBy,
                                          Responsible1 = dt.Responsible1,
                                          Responsible2 = dt.Responsible2,
                                          Release = dt.Release,
                                          EstimatedHours = dt.EstimatedHours,
                                          ActualHours = dt.ActualHours,
                                          FRSMenuLink = dt.FRSMenuLink,
                                          UrlOrMenuOrWorkflow = dt.UrlOrMenuOrWorkflow,
                                          Remarks = dt.Remarks,
                                          TaskCompletedTime = dt.TaskCompletedTime,
                                          TaskCreationDate = dt.TaskCreationDate,
                                          QAResponsible = dt.QAResponsible,
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
