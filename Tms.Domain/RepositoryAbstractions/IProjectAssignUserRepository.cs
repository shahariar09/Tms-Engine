using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entity;

namespace Tms.Domain.RepositoryAbstractions
{
    public interface IProjectAssignUserRepository
    {
        Task<ProjectUser> GetProjectAssignUserAsync(int UserId, int ProjectId);
        Task AddProjectAssignUserAsync(ProjectUser projectAssignUser);
        Task RemoveProjectAssignUserAsync(int userId, int projectId);
    }
}
