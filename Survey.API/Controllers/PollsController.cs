
using Survey.Core.Dtos.Poll.Requests;
using Survey.Core.Dtos.Poll.Responses;
using Survey.Entities.entities; // don't forget to remove referefce of entities project

namespace Survey.API.Controllers
{
    
    public class PollsController : ApiBaseController
    {
        private readonly IList<Poll> _polls = new List<Poll>()
        {
            new Poll()
            {
                Id = 1,
                Title = "Test",
                Description = "Test Des",
            }
        };

        [HttpGet]
        public async Task<IList<PollResponse>> GetPolls()
        {
            var polls = _polls.ToList();

            var pollsDTos = new List<PollResponse>();
            
            foreach (var poll in polls)
            {
                pollsDTos.Add(new PollResponse()
                {
                    Id = poll.Id,
                    Title = poll.Title,
                    Description = poll.Description,
                });
            }

            return pollsDTos;
        }

        [HttpGet("{id}")]
        public async Task<PollResponse> GetById(int id)
        {
            var poll = _polls.FirstOrDefault(x=> x.Id == id);

            var pollsDTo = new PollResponse()
            {
                Id = poll.Id,
                Title = poll.Title,
                Description = poll.Description,
            };

            return pollsDTo;
        }

        [HttpPost]
        public Task AddPoll(CreatePollRequest pollRequest)
        {
            var poll = new Poll()
            {
                Id = _polls.Count + 1,
                Title = pollRequest.Title,
                Description = pollRequest.Description,
            };
            
            _polls.Add(poll);

           return Task.CompletedTask;
        }

        [HttpPut("{id}")]
        public async Task<Poll> Update(int id,CreatePollRequest poll)
        {
            var query = _polls.FirstOrDefault(x => x.Id == id);

            query.Title = poll.Title;
            query.Description = poll.Description;

            return query;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var query = _polls.FirstOrDefault(x => x.Id == id);
            _polls.Remove(query);
            return true;
        }

    }
}
