namespace Survey.DataAccess.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPollRepositoryAsync PollRepository { get; }

        public IQuestionRepositoryAsync QuestionRepository { get; }

        public IAnswerRepositoryAsync AnswerRepository { get; }

        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            PollRepository = new PollRepositoryAsync(appDbContext);
            QuestionRepository = new QuestionRepositoryAsync(appDbContext);
            AnswerRepository = new AnswerRepositoryAsync(appDbContext);
            _appDbContext = appDbContext;
        }
        public async ValueTask DisposeAsync()
        {
            await _appDbContext.DisposeAsync();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
