

namespace Survey.Core.IServices
{
    public interface IPollService
    {
        Task<IList<PollResponse>> GetAll();
        Task<PollResponse> GetById(int id);
        Task AddPoll(CreatePollRequest poll);
        Task UpdatePoll(int pollId, CreatePollRequest poll);
        Task DeletePoll(int id);
    }
}
