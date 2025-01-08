using AutoMapper;
using Tms.Application.DTOs;
using Tms.Application.ServiceAbstractions;
using Tms.Domain.Entity;
using Tms.Domain.RepositoryAbstractions;
//using Tms.Application.Common.Exception;
using static Tailoring.Application.Common.Exceptions.ValidationException;

namespace Tms.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserTaskRepository _userTaskRepository;

        public TaskItemService(
            ITaskItemRepository taskItemRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IUserTaskRepository userTaskRepository)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTasksAsync()
        {
            var tasks = await _taskItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
        }

        public async Task<TaskItemDto> GetTaskByIdAsync(int id)
        {
            var task = await _taskItemRepository.GetByIdAsync(id);
            return _mapper.Map<TaskItemDto>(task);
        }

        public async Task<TaskItemDto> CreateTaskAsync(CreateTaskItemDto taskDto)
        {
            var taskItem = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Priority = taskDto.Priority,
                Status = taskDto.Status,
                DueDate = taskDto.DueDate,
                AssignedUsers = new List<UserTask>()
            };

            var createdTask = await _taskItemRepository.CreateAsync(taskItem);
            return _mapper.Map<TaskItemDto>(createdTask);
        }

        public async Task<TaskItemDto> UpdateTaskAsync(int id, CreateTaskItemDto taskDto)
        {
            var existingTask = await _taskItemRepository.GetByIdAsync(id);
            if (existingTask == null)
                throw new Exception("Task not found");

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.Priority = taskDto.Priority;
            existingTask.Status = taskDto.Status;
            existingTask.DueDate = taskDto.DueDate;

            var updatedTask = await _taskItemRepository.UpdateAsync(existingTask);
            return _mapper.Map<TaskItemDto>(updatedTask);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskItemRepository.DeleteAsync(id);
        }

        public async Task AssignUserToTask(int userId, int taskId)
        {
            try
            {
                // user exists check
                var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new BadRequestException("User not found");
            // task exists check
            var task = await _taskItemRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new BadRequestException("Task not found");
            // user is already assigned to the task or not 
            
            var existingAssignment = await _userTaskRepository.GetUserTaskAsync(userId, taskId);
            if (existingAssignment != null)
                throw new BadRequestException("User is already assigned to this task");

            // Assign the user to the task
            var userTask = new UserTask
            {
                UserId = userId,
                TaskItemId = taskId
            };
            try
            {
                // Add the new assignment to the UserTasks table
                await _userTaskRepository.AddUserTaskAsync(userTask);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save user task assignment: {ex.Message}", ex);
            }
        }
    catch (Exception ex)
    {
        throw new Exception($"Error in AssignUserToTask: {ex.Message}", ex);
    }
}
    }
}
