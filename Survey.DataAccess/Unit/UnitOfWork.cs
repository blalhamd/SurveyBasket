
namespace Survey.DataAccess.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPollRepositoryAsync PollRepository { get; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            PollRepository = new PollRepositoryAsync();
        }
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
