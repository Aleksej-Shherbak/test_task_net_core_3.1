using System.Linq;
using System.Threading.Tasks;
using Domains;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Repos.Abstract;

namespace Repos.Concrete
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public JobRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Job> All => _dbContext.Jobs;

        public async Task SaveAsync(Job job)
        {
            if (job.JobId == 0)
            {
                await _dbContext.AddAsync(job);
            }
            else
            {
                _dbContext.Attach(job).State = EntityState.Modified;
                _dbContext.Update(job);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dbEntity = await _dbContext.Jobs.FindAsync(id);

            if (dbEntity != null)
            {
                _dbContext.Jobs.Remove(dbEntity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}