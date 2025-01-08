using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.Domain.Entity;
using Tms.Domain.RepositoryAbstractions;

namespace Tms.Infrastructure.Data.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.TaskItems
                .Include(t => t.AssignedUsers)
                    .ThenInclude(ut => ut.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems
                .Include(t => t.AssignedUsers)
                    .ThenInclude(ut => ut.User)
                .ToListAsync();
        }


        public async Task<TaskItem> CreateAsync(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<TaskItem> UpdateAsync(TaskItem taskItem)
        {
            _context.Entry(taskItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null) return false;

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}