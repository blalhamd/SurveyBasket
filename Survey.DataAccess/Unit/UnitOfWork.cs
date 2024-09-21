
namespace Survey.DataAccess.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPollRepositoryAsync PollRepository { get; }
        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            PollRepository = new PollRepositoryAsync();
            _appDbContext = appDbContext;
        }
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollBackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
