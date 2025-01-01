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
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;

        public TaskItemService(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
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
    }
}