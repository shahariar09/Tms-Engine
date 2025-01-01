using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tms.Application.DTOs;
using Tms.Application.Services;



namespace Tms.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly TaskItemService _taskService;

        public TaskItemsController(TaskItemService taskService)
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
    }
}