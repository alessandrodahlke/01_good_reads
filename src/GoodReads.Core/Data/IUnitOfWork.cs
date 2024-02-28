namespace GoodReads.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
