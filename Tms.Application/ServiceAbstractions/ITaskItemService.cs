using System.Collections.Generic;
using System.Threading.Tasks;
using Tms.Application.DTOs;

namespace Tms.Application.ServiceAbstractions
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItemDto>> GetAllTasksAsync();
        Task<TaskItemDto> GetTaskByIdAsync(int id);
        Task<TaskItemDto> CreateTaskAsync(CreateTaskItemDto taskDto);
        Task<TaskItemDto> UpdateTaskAsync(int id, CreateTaskItemDto taskDto);
        Task<bool> DeleteTaskAsync(int id);
        Task AssignUserToTask(int userId, int taskId);
    }
     }