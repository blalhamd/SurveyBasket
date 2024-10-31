namespace Survey.Core.IServices
{
    public interface IPollService
    {
        Task<IList<PollResponse>> GetAll(CancellationToken cancellationToken);
        Task<IList<PollResponse>> GetCurrentPolls(CancellationToken cancellationToken);
        Task<PollResponse> GetById(int id, CancellationToken cancellationToken);
        Task AddPoll(CreatePollRequest poll, CancellationToken cancellationToken);
        Task UpdatePoll(int pollId, CreatePollRequest poll,CancellationToken cancellationToken);
        Task DeletePoll(int id, CancellationToken cancellationToken);
    }
}
