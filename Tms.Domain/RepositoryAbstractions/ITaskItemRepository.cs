
using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.Domain.Entity;

namespace Tms.Domain.RepositoryAbstractions
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem taskItem);
        Task<TaskItem> UpdateAsync(TaskItem taskItem);
        Task<bool> DeleteAsync(int id);
    }
}