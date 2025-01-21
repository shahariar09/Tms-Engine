using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.Application.DTOs;
using Tms.Application.Services;
using Tms.Application.ServiceAbstractions;
using static Tailoring.Application.Common.Exceptions.ValidationException;
using Tms.Domain.Entity;

namespace Tms.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskItemService _taskService;

        public TaskItemsController(ITaskItemService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTask(CreateTaskItemDto taskDto)
        {
            // Validate status
            if (!TaskStatusEnum.IsValidStatus(taskDto.Status))
            {
                return BadRequest($"Invalid status. Valid statuses are: {string.Join(", ", TaskStatusEnum.ValidStatuses)}");
            }

            // Normalize the status case
            taskDto.Status = TaskStatusEnum.ValidStatuses.First(s =>
                s.Equals(taskDto.Status, StringComparison.OrdinalIgnoreCase));

            var task = await _taskService.CreateTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, CreateTaskItemDto taskDto)
        {
            try
            {
                var task = await _taskService.UpdateTaskAsync(id, taskDto);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }


        [HttpPost("assign-user")]
        public async Task<IActionResult> AssignUserToTask([FromQuery] int userId, [FromQuery] int taskId)
        {
            try
            {
                await _taskService.AssignUserToTask(userId, taskId);
                return Ok(new { message = "User successfully assigned to task" });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the full exception details
                return StatusCode(500, new
                {
                    message = "An error occurred while assigning the user to the task",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }

        [HttpDelete("unassign-user")]
        public async Task<IActionResult> UnassignUserFromTask([FromQuery] int userId, [FromQuery] int taskId)
        {
            try
            {
                await _taskService.UnassignUserFromTask(userId, taskId);
                return Ok(new { message = "User successfully unassigned from task" });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the full exception details
                return StatusCode(500, new
                {
                    message = "An error occurred while unassigning the user from the task",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetProjectTasks(int projectId)
        {
            var tasks = await _taskService.GetProjectTasksAsync(projectId);
            return Ok(tasks);
        }

        [HttpGet("project/{projectId}/board")]
        public async Task<ActionResult<IDictionary<string, IEnumerable<TaskItemDto>>>> GetProjectTasksBoard(int projectId)
        {
            var taskBoard = await _taskService.GetProjectTasksBoardAsync(projectId);
            return Ok(taskBoard);
        }

        [HttpPatch("{taskId}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int taskId, [FromBody] string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                return BadRequest("Status cannot be empty");

            // Case-insensitive status validation
            if (!TaskStatusEnum.IsValidStatus(newStatus))
                return BadRequest($"Invalid status. Valid statuses are: {string.Join(", ", TaskStatusEnum.ValidStatuses)}");

            // Normalize the status to match the enum values
            string normalizedStatus = TaskStatusEnum.ValidStatuses.First(s =>
                s.Equals(newStatus, StringComparison.OrdinalIgnoreCase));

            var result = await _taskService.UpdateTaskStatusAsync(taskId, normalizedStatus);

            if (!result)
                return NotFound($"Task with ID {taskId} not found");

            return Ok(new { status = normalizedStatus });
        }


    }
}

