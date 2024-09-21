
namespace Survey.Core.IUnit
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IPollRepositoryAsync PollRepository { get; }
    }
}
