using AutoMapper;
using Tms.Application.DTOs;
using Tms.Application.DTOs.User;
using Tms.Domain.Entity;

namespace Tms.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReturnDto>();
           
            CreateMap<TaskItem, TaskItemDto>()
                    .ForMember(dest => dest.AssignedUsers, opt => opt.MapFrom(src => src.AssignedUsers));

            CreateMap<CreateTaskItemDto, TaskItem>()
                .ForMember(dest => dest.AssignedUsers, opt => opt.Ignore()); // Assuming you'll handle assignment separately.

            CreateMap<User, UserTaskDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name));


        }
    }
}
