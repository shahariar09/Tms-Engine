using AutoMapper;
using Tms.Application.DTOs.User;
using Tms.Application.ServiceAbstractions;
using Tms.Domain.RepositoryAbstractions;
using User = Tms.Domain.Entity.User;

namespace Tms.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;


        public UserService(IUserRepository UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _mapper = mapper;
        }

        public async Task<UserReturnDto> GetUserByIdAsync(int id)
        {
            var User = await _UserRepository.GetByIdAsync(id);
            if (User == null)
                throw new KeyNotFoundException("User not found");

            return _mapper.Map<UserReturnDto>(User);
        }

        public async Task<IEnumerable<UserReturnDto>> GetAllUsersAsync()
        {
            var Users = await _UserRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserReturnDto>>(Users);
        }

        public async Task AddUserAsync(UserCreateDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            await _UserRepository.AddAsync(user);
            await _UserRepository.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, UserCreateDto updateUserDto)
        {
            var User = await _UserRepository.GetByIdAsync(id);
            if (User == null)
                throw new KeyNotFoundException("User not found");

            _mapper.Map(updateUserDto, User);
            await _UserRepository.UpdateAsync(User);
            await _UserRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            await _UserRepository.DeleteAsync(id);
            await _UserRepository.SaveChangesAsync();
        }

    }
}
