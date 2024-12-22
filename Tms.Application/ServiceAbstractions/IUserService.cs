using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Application.DTOs.User;

namespace Tms.Application.ServiceAbstractions
{
    public interface IUserService
    {
        Task<UserReturnDto> GetUserByIdAsync(int id); // Retrieve a User by ID
        Task<IEnumerable<UserReturnDto>> GetAllUsersAsync(); // Retrieve all Users
        Task AddUserAsync(UserCreateDto createUserDto); // Add a new User
        Task UpdateUserAsync(int id, UserCreateDto updateUserDto); // Update an existing User
        Task DeleteUserAsync(int id); // Delete a User by ID
    }
}
