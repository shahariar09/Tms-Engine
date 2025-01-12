using AutoMapper;
using Tms.Application.DTOs;
using Tms.Application.ServiceAbstractions;
using Tms.Domain.Entity;
using Tms.Domain.RepositoryAbstractions;
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

        public async Task<IEnumerable<TaskItemDto>> GetProjectTasksAsync(int projectId)
        {
            var tasks = await _taskItemRepository.GetTasksByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
        }

        public async Task<IDictionary<string, IEnumerable<TaskItemDto>>> GetProjectTasksBoardAsync(int projectId)
        {
            var allTasks = await _taskItemRepository.GetTasksByProjectIdAsync(projectId);
            Console.WriteLine($"[Debug] Found {allTasks.Count()} tasks for project {projectId}");

            foreach (var task in allTasks)
            {
                Console.WriteLine($"[Debug] Task ID: {task.Id}, Title: {task.Title}, Status: {task.Status}");
            }

            var mappedTasks = _mapper.Map<IEnumerable<TaskItemDto>>(allTasks);

            foreach (var task in mappedTasks)
            {
                Console.WriteLine($"[Debug] Mapped Task ID: {task.Id}, Title: {task.Title}, Status: {task.Status}");
            }

            var result = TaskStatusEnum.ValidStatuses.ToDictionary(
                status => status,
                status => {
                    var tasksForStatus = mappedTasks.Where(t => string.Equals(t.Status, status, StringComparison.OrdinalIgnoreCase));
                    Console.WriteLine($"[Debug] Status {status}: {tasksForStatus.Count()} tasks");
                    return tasksForStatus;
                }
            );

            return result;
        }

        public async Task<bool> UpdateTaskStatusAsync(int taskId, string newStatus)
        {
            if (!TaskStatusEnum.ValidStatuses.Contains(newStatus))
                return false;

            return await _taskItemRepository.UpdateTaskStatusAsync(taskId, newStatus);
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
            if (!TaskStatusEnum.IsValidStatus(taskDto.Status))
            {
                throw new BadRequestException($"Invalid status. Valid statuses are: {string.Join(", ", TaskStatusEnum.ValidStatuses)}");
            }

            var taskItem = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Priority = taskDto.Priority,
                Status = TaskStatusEnum.ValidStatuses.First(s =>
                    s.Equals(taskDto.Status, StringComparison.OrdinalIgnoreCase)),
                DueDate = taskDto.DueDate,
                ProjectId = taskDto.ProjectId,
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
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new BadRequestException("User not found");

                var task = await _taskItemRepository.GetByIdAsync(taskId);
                if (task == null)
                    throw new BadRequestException("Task not found");

                var existingAssignment = await _userTaskRepository.GetUserTaskAsync(userId, taskId);
                if (existingAssignment != null)
                    throw new BadRequestException("User is already assigned to this task");

                var userTask = new UserTask
                {
                    UserId = userId,
                    TaskItemId = taskId
                };

                await _userTaskRepository.AddUserTaskAsync(userTask);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AssignUserToTask: {ex.Message}", ex);
            }
        }

        public async Task UnassignUserFromTask(int userId, int taskId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new BadRequestException($"User with ID {userId} not found");

            var task = await _taskItemRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new BadRequestException($"Task with ID {taskId} not found");

            var existingAssignment = await _userTaskRepository.GetUserTaskAsync(userId, taskId);
            if (existingAssignment == null)
                throw new BadRequestException($"User {userId} is not assigned to task {taskId}");

            var result = await _userTaskRepository.RemoveUserTaskAsync(userId, taskId);
            if (!result)
                throw new Exception("Failed to remove user task assignment");
        }
    }
}
