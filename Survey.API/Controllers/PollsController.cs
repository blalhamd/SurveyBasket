

namespace Survey.API.Controllers
{
    
    public class PollsController : ApiBaseController
    {
        private readonly IPollService _pollService;

        public PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet]
        public async Task<IList<PollResponse>> GetPolls()
        {
            return await _pollService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<PollResponse> GetById(int id)
        {
            return await _pollService.GetById(id);
        }

        [HttpPost]
        public async Task AddPoll(CreatePollRequest pollRequest)
        {
            await _pollService.AddPoll(pollRequest);
        }

        [HttpPut("{id}")]
        public async Task Update(int id,CreatePollRequest poll)
        {
            await _pollService.UpdatePoll(id,poll);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _pollService.DeletePoll(id);
        }

    }
}
