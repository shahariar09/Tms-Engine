using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Application.DTOs;

namespace Tms.Application.ServiceAbstractions
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(int id);
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto);
        Task<ProjectDto> UpdateProjectAsync(int id, CreateProjectDto projectDto);
        Task<bool> DeleteProjectAsync(int id);

        Task AssignProjectToUser(int projectId, int userId);
        Task UnassignUserFromProject(int projectId, int userId);

    }
}
