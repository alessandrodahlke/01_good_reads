using GoodReads.Core.DomainObjects;

namespace GoodReads.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAgreggateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
