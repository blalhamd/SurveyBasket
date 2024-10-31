namespace Survey.API.Controllers
{
    [EnableRateLimiting(policyName: RateLimiterType.Concurrency)]
    public class QuestionsController : ApiBaseController
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("{PollId}")]
        public async Task AddQuestion(int PollId, QuestionRequest question, CancellationToken cancellationToken = default)
        {
            await _questionService.AddQuestion(PollId, question, cancellationToken);
        }

        [Cached(duration: 600)]
        [HttpGet("{pollId}")]
        public async Task<ActionResult<IList<QuestionResponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken = default)
        {
            return Ok(await _questionService.GetAllAsync(pollId, cancellationToken));
        }

        [Cached(duration: 600)]
        [HttpGet("{pollId}/{id}")]
        public async Task<ActionResult<QuestionResponse>> GetByIdAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            return Ok(await _questionService.GetByIdAsync(pollId, id, cancellationToken));
        }

        [HttpPut("{pollId}/{id}/UpdateQuestionAsync")]
        public async Task UpdateQuestionAsync(int pollId, int id, QuestionRequest questionRequest, CancellationToken cancellationToken = default)
        {
            await _questionService.UpdateQuestionAsync(pollId, id, questionRequest, cancellationToken);
        }

        [HttpGet("{pollId}/{id}/ToggleQuestionStatus")]
        public async Task<bool> ToggleQuestionStatus(int pollId, int id, CancellationToken cancellationToken = default)
        {
            return await _questionService.ToggleQuestionStatus(pollId, id, cancellationToken);
        }
    }
}
