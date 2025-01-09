using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.Domain.Entity;

namespace Tms.Domain.RepositoryAbstractions
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task<bool> DeleteAsync(int id);
        //Include Assigned Users in GetAllProjects and GetProjectById
        Task<IEnumerable<Project>> GetAllProjectsWithUsersAsync();
        Task<Project> GetProjectWithUsersByIdAsync(int id);

    }
}
