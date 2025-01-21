using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.Application.DTOs;
using Tms.Application.DTOs.Project;
using Tms.Application.ServiceAbstractions;
using Tms.Application.Services;

namespace Tms.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectDto projectDto)
        {
            var project = await _projectService.CreateProjectAsync(projectDto);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, CreateProjectDto projectDto)
        {
            try
            {
                var updatedProject = await _projectService.UpdateProjectAsync(id, projectDto);
                return Ok(updatedProject);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }


        //assign project to user
        //[HttpPost("assign-user")]
        //public async Task<IActionResult> AssignUserToProject(ProjectAssignUserDto dto)
        //{
        //    try
        //    {
        //        await _projectService.AssignProjectToUser(dto.ProjectId, dto.UserIds);
        //        return Ok(new { message = "User successfully assigned to project" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            message = "An error occurred while assigning the user to the project",
        //            error = ex.Message,
        //            innerError = ex.InnerException?.Message
        //        });
        //    }
        //}
        [HttpPost("assign-user")]
        public async Task<IActionResult> AssignUserToProject(ProjectAssignUserDto dto)
        {
            if (dto.UserIds == null || !dto.UserIds.Any())
            {
                return BadRequest(new { message = "UserIds cannot be null or empty." });
            }

            try
            {
                foreach (var userId in dto.UserIds)
                {
                    await _projectService.AssignProjectToUser(dto.ProjectId, userId);
                }

                return Ok(new { message = "Users successfully assigned to project." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while assigning the user to the project.",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }



        [HttpDelete("unassign-user")]
        public async Task<IActionResult> UnassignUserFromProject([FromQuery] int projectId, [FromQuery] int userId)
        {
            try
            {
                await _projectService.UnassignUserFromProject(projectId, userId);
                return Ok(new { message = "User successfully unassigned from the project" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while unassigning the user from the project",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }

    }
}
