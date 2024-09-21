
namespace Survey.Core.IUnit
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IPollRepositoryAsync PollRepository { get; }
        Task<int> SaveAsync();
        Task CommitAsync();
        Task RollBackAsync();
    }
}
