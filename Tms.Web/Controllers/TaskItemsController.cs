using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.Application.DTOs;
using Tms.Application.Services;
using Tms.Application.ServiceAbstractions;
using static Tailoring.Application.Common.Exceptions.ValidationException;

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

        //[HttpPost("assign")]
        //public async Task<IActionResult> AssignUserToTask([FromBody] UserTaskAssignmentDto assignment)
        //{
        //    try
        //    {
        //        await _taskService.AssignUserToTask(assignment.UserId, assignment.TaskId);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

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

    }
}
