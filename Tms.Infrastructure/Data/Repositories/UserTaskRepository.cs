using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tms.Domain.Entity;
using Tms.Domain.RepositoryAbstractions;
using Tms.Infrastructure;

namespace Tms.Infrastructure.Data.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public UserTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserTask> GetUserTaskAsync(int userId, int taskId)
        {
            return await _context.UserTasks
                .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TaskItemId == taskId);
        }

        public async Task AddUserTaskAsync(UserTask userTask)
        {
            try
            {
                await _context.UserTasks.AddAsync(userTask);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add user task: {ex.Message}", ex);
            }
        }
    }
}

