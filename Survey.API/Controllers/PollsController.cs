namespace Survey.API.Controllers
{
    [EnableRateLimiting(policyName: RateLimiterType.Concurrency)]
    public class PollsController : ApiBaseController
    {
        private readonly IPollService _pollService;

        public PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [Cached(duration: 600)]
        [HttpGet]
        [Authorize(Roles = $"{DefaultRole.Admin},{DefaultRole.Member}")]
        // [DisableCors]
        //  [EnableCors(PolicyName.AdminPolicy)]
        public async Task<ActionResult<IList<PollResponse>>> GetPolls(CancellationToken cancellationToken)
        {
            return Ok(await _pollService.GetAll(cancellationToken));
        }

        [Cached(duration: 600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PollResponse>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _pollService.GetById(id, cancellationToken));
        }

        [Cached(duration: 600)]
        [HttpGet("CurrentPolls")]
        [EnableRateLimiting(policyName: RateLimiterType.UserLimiting)]
        public async Task<ActionResult<IList<PollResponse>>> GetCurrentPolls(CancellationToken cancellationToken)
        {
            return Ok(await _pollService.GetCurrentPolls(cancellationToken));
        }

        [HttpPost]
        public async Task AddPoll(CreatePollRequest pollRequest, CancellationToken cancellationToken)
        {
            await _pollService.AddPoll(pollRequest,cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task Update(int id,CreatePollRequest poll, CancellationToken cancellationToken)
        {
            await _pollService.UpdatePoll(id,poll, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _pollService.DeletePoll(id, cancellationToken);
        }

    }
}
