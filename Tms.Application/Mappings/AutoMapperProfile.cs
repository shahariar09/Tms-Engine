using AutoMapper;
using Tms.Application.DTOs;
using Tms.Application.DTOs.User;
using Tms.Domain.Entity;
using System.Linq;

namespace Tms.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapping for User
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReturnDto>();

            // Mapping for Project
            CreateMap<Project, ProjectDto>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<ProjectUser, ProjectUserDto>();

            // Mapping for TaskItem to TaskItemDto with AssignedUsers
            CreateMap<TaskItem, TaskItemDto>()
                .ForMember(dest => dest.AssignedUsers, opt => opt.MapFrom(src =>
                    src.AssignedUsers.Select(au => new UserTaskDto
                    {
                        UserId = au.User.Id,
                        UserName = au.User.Name
                    })));

            // Mapping for CreateTaskItemDto to TaskItem
            CreateMap<CreateTaskItemDto, TaskItem>()
                .ForMember(dest => dest.AssignedUsers, opt => opt.Ignore()); // Handle assignment separately

            // Mapping for User to UserTaskDto
            CreateMap<User, UserTaskDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name));

            // Mapping for UserTask to UserTaskAssignmentDto
            CreateMap<UserTask, UserTaskAssignmentDto>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskItemId));

            // Mapping for UserTaskAssignmentDto to UserTask
            CreateMap<UserTaskAssignmentDto, UserTask>()
                .ForMember(dest => dest.TaskItemId, opt => opt.MapFrom(src => src.TaskId));
        }
    }
}
