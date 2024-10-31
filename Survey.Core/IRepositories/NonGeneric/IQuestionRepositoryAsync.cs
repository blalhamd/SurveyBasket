namespace Survey.Core.IRepositories.NonGeneric
{
    public interface IQuestionRepositoryAsync : IGenericRepositoryAsync<Question>
    {
        Task<IList<QuestionResponse>> GetAllQuestions(int pollId,CancellationToken cancellationToken = default);
        Task<QuestionResponse> GetQuestionById(int pollId,int id,CancellationToken cancellationToken = default);
        Task<IList<QuestionResponse>> GetAvailableAsync(int pollId, int userId, CancellationToken cancellationToken = default);
    }
}
