using GoodReadas.Users.Domain.Entities;
using GoodReadas.Users.Domain.Repositories;
using GoodReads.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace GoodReads.Users.Infra.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;
        public UserRepository(UsersContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Add(User user)
        {
           await _context.Users.AddAsync(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Address == email);
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Dispose()
        {
           _context?.Dispose();
        }
    }
}
