using System.Linq;
using System.Threading.Tasks;
using Domains;

namespace Repos.Abstract
{
    public interface IJobRepository
    {
        IQueryable<Job> All { get; }

        Task SaveAsync(Job job);

        Task DeleteAsync(int id);
    }
}