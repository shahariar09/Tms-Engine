using AutoMapper;
using Tms.Application.DTOs.User;
using Tms.Application.ServiceAbstractions;
using Tms.Domain.RepositoryAbstractions;
using Tms.Domain.Entity;
using System.Security.Cryptography;
using System.Text;

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

        public async Task<string> AddUserAsync(UserCreateDto createUserDto)
        {
            var tempPassword = GenerateTemporaryPassword();
            var salt = GenerateSalt();
            var hashedPassword = HashPassword(tempPassword, salt);

            var user = _mapper.Map<User>(createUserDto);
            user.PasswordHash = hashedPassword;
            user.Salt = salt;
            user.IsTempPassword = true;

            await _UserRepository.AddAsync(user);
            await _UserRepository.SaveChangesAsync();

            return tempPassword;
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

        // Helper Methods for Password Generation
        private string GenerateTemporaryPassword()
        {
            // Generate a temporary 4-digit password
            return new Random().Next(1000, 9999).ToString();
        }

        private string GenerateSalt()
        {
            // Generate a 16-byte random salt and convert to base64
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combined = Encoding.UTF8.GetBytes(password + salt); // Combine password and salt
                return Convert.ToBase64String(sha256.ComputeHash(combined)); // Return hashed password
            }
        }

        // Implement LoginAsync method
        //public async Task<string> LoginAsync(LoginDto loginDto)
        //{
        //    var user = await _UserRepository.GetByIdAsync(loginDto.UserId); // Use GetByIdAsync method
        //    if (user == null)
        //        throw new UnauthorizedAccessException("Invalid username or password");

        //    var hashedPassword = HashPassword(loginDto.Password, user.Salt); // Hash the entered password with the stored salt
        //    if (hashedPassword != user.PasswordHash)
        //        throw new UnauthorizedAccessException("Invalid username or password");

        //    return "Login successful";
        //}

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            // Fetch the user by Email instead of UserId
            var user = await _UserRepository.GetByEmailAsync(loginDto.Email); // Use a new method to fetch by email
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            // Hash the entered password using the user's stored salt
            var hashedPassword = HashPassword(loginDto.Password, user.Salt);
            if (hashedPassword != user.PasswordHash)
                throw new UnauthorizedAccessException("Invalid email or password");

            return "Login successful";
        }


        // Implement ChangePasswordAsync method
        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await _UserRepository.GetByIdAsync(changePasswordDto.UserId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var currentHashedPassword = HashPassword(changePasswordDto.CurrentPassword, user.Salt);
            if (currentHashedPassword != user.PasswordHash)
                throw new UnauthorizedAccessException("Current password is incorrect");

            var newSalt = GenerateSalt();
            var newHashedPassword = HashPassword(changePasswordDto.NewPassword, newSalt);

            user.PasswordHash = newHashedPassword;
            user.Salt = newSalt;
            await _UserRepository.UpdateAsync(user);
            await _UserRepository.SaveChangesAsync();
        }
    }
}
