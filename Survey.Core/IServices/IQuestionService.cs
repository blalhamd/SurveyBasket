namespace Survey.Core.IServices
{
    public interface IQuestionService
    {
        Task<IList<QuestionResponse>> GetAllAsync(int pollId,CancellationToken cancellationToken = default);
        Task<IList<QuestionResponse>> GetAvailableAsync(int pollId,int userId, CancellationToken cancellationToken = default);
        Task<QuestionResponse> GetByIdAsync(int pollId,int id,CancellationToken cancellationToken = default);
        Task AddQuestion(int PollId,QuestionRequest question,CancellationToken cancellationToken = default);
        Task UpdateQuestionAsync(int pollId, int id, QuestionRequest question,CancellationToken cancellationToken = default);
        Task<bool> ToggleQuestionStatus(int pollId,int id,CancellationToken cancellationToken = default); 
    }
}
