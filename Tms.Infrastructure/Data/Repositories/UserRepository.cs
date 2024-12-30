using Microsoft.EntityFrameworkCore;
using Tms.Domain.RepositoryAbstractions;
using User = Tms.Domain.Entity.User;

namespace Tms.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users
                .Include(c => c.Role)
                .FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users
                .Include(c=>c.Role)
                .ToListAsync();
        }

        public async Task AddAsync(User User)
        {
            await _dbContext.Users.AddAsync(User);
        }

        public async Task UpdateAsync(User User)
        {
            _dbContext.Users.Update(User);
        }

        public async Task DeleteAsync(int id)
        {
            var User = await GetByIdAsync(id);
            if (User != null)
            {
                _dbContext.Users.Remove(User);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
