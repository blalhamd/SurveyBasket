namespace Survey.API.Controllers
{
    [Route("api/polls/{pollId}/vote")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting(policyName: RateLimiterType.Concurrency)]
    public class VotesController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public VotesController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IList<QuestionResponse>> Start(int pollId, CancellationToken cancellationToken = default)
        {
            var userId = User.GetUserId(); 

            return await _questionService.GetAvailableAsync(pollId, Convert.ToInt32(userId), cancellationToken);
        }

    }
}
