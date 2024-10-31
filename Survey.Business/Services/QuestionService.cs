namespace Survey.Business.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task AddQuestion(int pollId, QuestionRequest question, CancellationToken cancellationToken = default)
        {
            _loggerService.LogInfo("Attempting to add a question to Poll #{ID}",pollId);

            var poll = await GetPollByIdOrThrowException(pollId, cancellationToken);

            if (question is null)
            {
                _loggerService.LogWarning("Question data is null.");
                throw new BadRequest("Data is null.");
            }

            var isExist = await CheckQuestionExist(question, cancellationToken);

            if (isExist)
            {
                _loggerService.LogWarning("Question already exists.");
                throw new ItemAlreadyExist("Question already exists.");
            }

            var newQuestion = _mapper.Map<Question>(question);
            newQuestion.PollId = pollId;

            await _unitOfWork.QuestionRepository.AddAsync(newQuestion, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            _loggerService.LogInfo($"Question '{newQuestion.Id}' added successfully.");
        }

        public async Task<IList<QuestionResponse>> GetAllAsync(int pollId, CancellationToken cancellationToken = default)
        {
            _loggerService.LogInfo("Fetching all questions for Poll #{ID}", pollId);

            Poll poll =  await GetPollFromRepo(pollId,cancellationToken);

            IList<QuestionResponse> questionResponses = await GetQuestionsFromRepo(pollId, cancellationToken);

            _loggerService.LogInfo($"Found {questionResponses.Count} questions for Poll ID: {pollId}.");
         
            return questionResponses;
        }

        public async Task<QuestionResponse> GetByIdAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            _loggerService.LogInfo("Fetching question with #{id} for Poll #{pollId}.", id, pollId);

            var query = await _unitOfWork.QuestionRepository.GetQuestionById(pollId, id, cancellationToken);

            if (query is null)
            {
                _loggerService.LogError("No exist question with the specified Poll ID and Question ID.");
                throw new ItemNotFound("No exist question with this ID or Poll ID.");
            }

            _loggerService.LogInfo($"Question with ID: {id} fetched successfully.");

            return query;
        }

        public async Task<IList<QuestionResponse>> GetAvailableAsync(int pollId, int userId, CancellationToken cancellationToken = default)
        {
            _loggerService.LogInfo("Fetching available questions for Poll ID: #{pollId} and User ID: #{userId}.",pollId,userId);

            var query = await _unitOfWork.QuestionRepository.GetAvailableAsync(pollId, userId, cancellationToken);

            if (query is null || !query.Any())
            {
                _loggerService.LogError("No available questions found.");
                throw new ItemNotFound("Not available questions.");
            }

            _loggerService.LogInfo($"Found {query.Count} available questions for User ID: {userId}.");
            return query;
        }

        public async Task UpdateQuestionAsync(int pollId, int id, QuestionRequest questionRequest, CancellationToken cancellationToken = default)
        {
            _loggerService.LogInfo("Updating question with ID: #{id} for Poll ID: #{pollId}.", id, pollId);

            var poll = await GetPollByIdOrThrowException(pollId, cancellationToken);

            var spec = new BaseSpecification<Question>
            {
                Predicate = x => x.Id == id,
                Includes = ["Answers"]
            };

            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(spec, cancellationToken);

            if (question is null)
            {
                _loggerService.LogError("Question with ID: #{id} not found.", id);
                throw new ItemNotFound("No exist question with this ID.");
            }

             await MappingQuestionRequestToQuestion(question,questionRequest);

             await _unitOfWork.QuestionRepository.UpdateAsync(question);
             await _unitOfWork.SaveAsync(cancellationToken);

            _loggerService.LogInfo("Question with ID: #{id} updated successfully.", id);
        }

        public async Task<bool> ToggleQuestionStatus(int pollId, int id, CancellationToken cancellationToken = default)
        {
            _loggerService.LogInfo("Toggling status for question with ID: #{id} in Poll ID: #{pollId}.", id, pollId);

            var poll = await _unitOfWork.PollRepository.GetByIdAsync(pollId, cancellationToken);

            if (poll is null)
            {
                _loggerService.LogError("Poll with ID: #{pollId} not found.", pollId);
                throw new ItemNotFound("No exist question with this Poll ID.");
            }

            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(id, cancellationToken);

            if (question is null)
            {
                _loggerService.LogError("Question with ID: #{id} not found.", id);
                throw new ItemNotFound("No exist question with this ID.");
            }

            question.IsActive = !question.IsActive;

            await _unitOfWork.SaveAsync(cancellationToken);
            _loggerService.LogInfo("Question with ID: #{id} status toggled to: {question.IsActive}.", id);

            return true;
        }

        private async Task<Poll> GetPollByIdOrThrowException(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _unitOfWork.PollRepository.GetByIdAsync(id, cancellationToken);

            if (poll is null)
            {
                _loggerService.LogError("Poll with ID: #{pollId} not found.", id);
                throw new ItemNotFound("No exist question with this Poll ID.");
            }

            return poll;
        }

        private async Task<bool> CheckQuestionExist(QuestionRequest question,CancellationToken cancellationToken = default)
        {
            var spec = new BaseSpecification<Question>
            {
                Predicate = (x => x.Content == question.Content)
            };

            var isExist = await _unitOfWork.QuestionRepository.GetByIdAsync(spec, cancellationToken);

            return isExist != null;
        }

        private Task MappingQuestionRequestToQuestion(Question question,QuestionRequest questionRequest)
        {
            question.Content = questionRequest.Content;

            if (questionRequest.answers is not null && question.Answers is not null)
            {
                foreach (var newAnswerContent in questionRequest.answers)
                {
                    var isAnswerExist = question.Answers.FirstOrDefault(a => a.Content == newAnswerContent);

                    if (isAnswerExist != null)
                    {
                        isAnswerExist.Content = newAnswerContent;
                    }
                    else
                    {
                        question.Answers.Add(new Answer() { Content = newAnswerContent });
                    }
                }

                var answersToRemove = question.Answers
                    .Where(a => !questionRequest.answers.Contains(a.Content))
                    .ToList();

                foreach (var answer in answersToRemove)
                {
                    var a = question.Answers.FirstOrDefault(x => x.Id == answer.Id);

                    if (a != null)
                    {
                        a.IsActive = false;
                    }
                }
            }

            return Task.CompletedTask;
        }

        private async Task<Poll> GetPollFromRepo(int pollId,CancellationToken cancellationToken = default)
        {
            _loggerService.LogInfo("Get Poll From Repo [Database]");
            var poll = await GetPollByIdOrThrowException(pollId, cancellationToken);

            return poll;
        }

        private async Task<IList<QuestionResponse>> GetQuestionsFromRepo(int pollId, CancellationToken cancellationToken = default)
        {
            _loggerService.LogInfo("Get Questions From Repo [Database] with PollId: {polldId}",pollId);

            var questionResponses = await _unitOfWork.QuestionRepository.GetAllQuestions(pollId, cancellationToken);

            if (questionResponses is null || !questionResponses.Any())
            {
                _loggerService.LogError("No questions found for the specified Poll ID.");
                throw new ItemNotFound("No questions exist.");
            }

            return questionResponses;
        }
    }
}