using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.Domain.Entity;
using Tms.Domain.RepositoryAbstractions;

namespace Tms.Infrastructure.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Project>> GetAllAsync()
        //{
        //    return await _context.Projects
        //        .Include(p => p.ProjectAssignUsers)
        //        .ThenInclude(pu => pu.User)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<IQueryable<Project>> GetAllProjectsAsync()
        {
            var result =  _context.Projects
                .Include(p => p.ProjectUsers)
                .ThenInclude(pu => pu.User)
                .AsQueryable();
            return result;
        }


        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
               .Include(p => p.ProjectUsers) 
               .ThenInclude(pu => pu.User)        
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get all projects with users
        public async Task<IEnumerable<Project>> GetAllProjectsWithUsersAsync()
        {
            return await _context.Projects
                .Include(p => p.ProjectUsers) 
                .ThenInclude(pu => pu.User)        
                .ToListAsync();
        }

        //  Get a single project with users by ID
        public async Task<Project> GetProjectWithUsersByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.ProjectUsers)
                .ThenInclude(pu => pu.User) // Include related User entity
                .FirstOrDefaultAsync(p => p.Id == id);
        }



    }
}
