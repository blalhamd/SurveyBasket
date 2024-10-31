
namespace Survey.Core.IUnit
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IPollRepositoryAsync PollRepository { get; }
        public IQuestionRepositoryAsync QuestionRepository { get; }
        public IAnswerRepositoryAsync AnswerRepository { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task CommitAsync();
        Task RollBackAsync();
    }
}
