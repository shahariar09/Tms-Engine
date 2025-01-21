using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entity;

namespace Tms.Domain.RepositoryAbstractions
{
    public interface IUserRepository
    {
        Task AddAsync(User user); 
        Task UpdateAsync(User user); 
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<User> GetByEmailAsync(string email);


    }
}
