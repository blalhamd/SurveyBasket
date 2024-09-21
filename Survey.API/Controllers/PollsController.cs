
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
        public async Task<IList<Poll>> GetPolls()
        {
            
            return _polls.ToList();
        }

        [HttpGet("{id}")]
        public async Task<Poll> GetById(int id)
        {
            return _polls.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task<Poll> AddPoll(Poll poll)
        {
            _polls.Add(poll);

            return _polls.LastOrDefault();
        }

        [HttpPut("{id}")]
        public async Task<Poll> Update(int id,Poll poll)
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
