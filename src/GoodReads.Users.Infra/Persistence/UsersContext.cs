using GoodReadas.Users.Domain.Entities;
using GoodReads.Core.Data;
using GoodReads.Core.DomainObjects;
using GoodReads.Core.DomainObjects.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GoodReads.Users.Infra.Persistence
{
    public class UsersContext : DbContext, IUnitOfWork
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Email>();
            modelBuilder.Ignore<Entity>();
        }

        public async Task<bool> Commit()
        {
            var result = await base.SaveChangesAsync();

            return result > 0;
        }
    }
}
