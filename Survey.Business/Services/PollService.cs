using Survey.Core.Dtos.Poll.Requests;
using Survey.Core.Dtos.Poll.Responses;
using Survey.Core.IServices;


namespace Survey.Business.Services
{
    public class PollService : IPollService
    {
        public Task AddPoll(CreatePollRequest poll)
        {
            throw new NotImplementedException();
        }

        public Task DeletePoll(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<PollResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PollResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePoll(int pollId, CreatePollRequest poll)
        {
            throw new NotImplementedException();
        }
    }
}
