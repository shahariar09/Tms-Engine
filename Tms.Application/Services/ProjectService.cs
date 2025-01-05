using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Tms.Application.DTOs;
using Tms.Application.ServiceAbstractions;
using Tms.Domain.Entity;
using Tms.Domain.RepositoryAbstractions;

namespace Tms.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Project not found");

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto)
        {
            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                DueDate = projectDto.DueDate
            };

            var createdProject = await _projectRepository.CreateAsync(project);
            return _mapper.Map<ProjectDto>(createdProject);
        }

        public async Task<ProjectDto> UpdateProjectAsync(int id, CreateProjectDto projectDto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                throw new Exception("Project not found");

            existingProject.Name = projectDto.Name;
            existingProject.Description = projectDto.Description;
            existingProject.DueDate = projectDto.DueDate;

            var updatedProject = await _projectRepository.UpdateAsync(existingProject);
            return _mapper.Map<ProjectDto>(updatedProject);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Project not found");

            return await _projectRepository.DeleteAsync(id);
        }
    }
}
