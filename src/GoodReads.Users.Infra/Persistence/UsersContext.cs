using GoodReadas.Users.Domain.Entities;
using GoodReads.Core.Data;
using GoodReads.Core.DomainObjects;
using GoodReads.Core.Mediator;
using GoodReads.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace GoodReads.Users.Infra.Persistence
{
    public class UsersContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public UsersContext(DbContextOptions<UsersContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersContext).Assembly);

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<DomainEvent>();

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.RemoveAllEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
