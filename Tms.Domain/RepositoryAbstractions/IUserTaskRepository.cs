using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entity;

namespace Tms.Domain.RepositoryAbstractions
{
    public interface IUserTaskRepository
    {
        Task<UserTask> GetUserTaskAsync(int userId, int taskId);
        Task AddUserTaskAsync(UserTask userTask);
        Task<bool> RemoveUserTaskAsync(int userId, int taskId);
    }
}
