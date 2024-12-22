using AutoMapper;
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
        }
    }
}
