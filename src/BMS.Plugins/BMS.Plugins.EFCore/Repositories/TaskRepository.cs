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
        public async Task<(IEnumerable<DevTaskDto>, int)> LoadTaskAsync(CancellationToken token = default)
        {
            try
            {
                //Expression<Func<DevTaskDto, bool>> deletePredicate;
                //Expression<Func<DevTaskDto, bool>> conditionPredicate;

                int recordsCount = await _context.Tasks
                //.Where(deletePredicate)
                //.Where(conditionPredicate)
                .CountAsync(cancellationToken: token);

                var taskList = await (from dt in _context.Tasks
                                      join p in _context.Projects on dt.ProjectId equals p.Id into dtp
                                      from p in dtp.DefaultIfEmpty()
                                      join r1 in _context.Users on dt.Responsible1Id equals r1.Id into dtr1
                                      from r1 in dtr1.DefaultIfEmpty()
                                      join r2 in _context.Users on dt.Responsible2Id equals r2.Id into dtr2
                                      from r2 in dtr2.DefaultIfEmpty()
                                      join cb in _context.Users on dt.CreatedById.ToString() equals cb.Id into dtcb
                                      from cb in dtcb.DefaultIfEmpty()
                                      join lmb in _context.Users on dt.LastModifiedById.ToString() equals lmb.Id into dtlmb
                                      from lmb in dtlmb.DefaultIfEmpty()
                                      join qar in _context.Users on dt.QAResponsibleId equals qar.Id into dtqar
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
