using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Application.DTOs;

namespace Tms.Application.ServiceAbstractions
{
    internal interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(int id);
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto);
        Task<ProjectDto> UpdateProjectAsync(int id, CreateProjectDto projectDto);
        Task<bool> DeleteProjectAsync(int id);
    }
}
