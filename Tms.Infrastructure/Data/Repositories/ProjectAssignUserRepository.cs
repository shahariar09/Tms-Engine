using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tms.Domain.Entity;
using Tms.Domain.RepositoryAbstractions;
using Tms.Infrastructure;

namespace Tms.Infrastructure.Data.Repositories
{
    public class ProjectAssignUserRepository : IProjectAssignUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectAssignUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectUser> GetProjectAssignUserAsync(int userId, int projectId)
        {
            return await _context.ProjectAssignUsers
                .FirstOrDefaultAsync(pu => pu.UserId == userId && pu.ProjectId == projectId);
        }

        public async Task AddProjectAssignUserAsync(ProjectUser projectAssignUser)
        {
            try
            {
                await _context.ProjectAssignUsers.AddAsync(projectAssignUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to assign user to project: {ex.Message}", ex);
            }
        }

        public async Task RemoveProjectAssignUserAsync(int userId, int projectId)
        {
            try
            {
                var projectAssignUser = await GetProjectAssignUserAsync(userId, projectId);
                if (projectAssignUser != null)
                {
                    _context.ProjectAssignUsers.Remove(projectAssignUser);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to remove user from project: {ex.Message}", ex);
            }
        }

        public async Task DeleteProjectAssignUserAsync(ProjectUser projectAssignUser)
        {
            if (projectAssignUser != null)
            {
                _context.ProjectAssignUsers.Remove(projectAssignUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
