namespace Survey.Business.Services
{
    public class PollService : IPollService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        public PollService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<IList<PollResponse>> GetAll(CancellationToken cancellationToken)
        {
            _loggerService.LogInfo("Fetching all polls.");

            var polls = await _unitOfWork.PollRepository.GetAllAsync(cancellationToken);

            if(polls is null)
            {
                _loggerService.LogInfo($"not exist polls");
                throw new ItemNotFound("not exist polls");
            }

            _loggerService.LogInfo($"Found {polls.Count} polls.");

            var pollsDtos = _mapper.Map<IList<PollResponse>>(polls);

            return pollsDtos;
        }

        public async Task<IList<PollResponse>> GetCurrentPolls(CancellationToken cancellationToken)
        {
            var spec = new PollSpecification(p => p.IsPublished &&
                                             p.StartsAt < DateOnly.FromDateTime(DateTime.Now) &&
                                             p.EndsAt > DateOnly.FromDateTime(DateTime.Now));

            var polls = await _unitOfWork.PollRepository.GetAllAsync(spec,cancellationToken);

            if (polls is null)
            {
                _loggerService.LogInfo($"not exist polls");
                throw new ItemNotFound("not exist polls");
            }

            var pollResponses = _mapper.Map<IList<PollResponse>>(polls);

            return pollResponses;
        }

        public async Task<PollResponse> GetById(int id, CancellationToken cancellationToken)
        {
            _loggerService.LogInfo("Fetching poll with #{id}.", id);

            var poll = await GetByIdOrThrowException(id, cancellationToken);

            _loggerService.LogInfo("Poll with #{id} found.", id);

            var pollResponse = _mapper.Map<PollResponse>(poll);

            return pollResponse;
        }


        public async Task AddPoll(CreatePollRequest pollDto, CancellationToken cancellationToken)
        {
            _loggerService.LogInfo("Adding a new poll.");

            if (pollDto is null)
            {
                _loggerService.LogError("Poll data is null.");
                throw new BadRequest("Data is Null"); 
            }

            CheckValidateDate(pollDto);

            var IsExist = await CheckPollExist(pollDto, cancellationToken);

            if (IsExist)
            {
                _loggerService.LogWarning("Poll already exists.");
                throw new ItemAlreadyExist("Item Already Exist");
            }

            var Poll = _mapper.Map<Poll>(pollDto);

            await _unitOfWork.PollRepository.AddAsync(Poll, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            _loggerService.LogInfo($"Poll '{Poll.Title}' added successfully.");
        }

        public async Task UpdatePoll(int pollId, CreatePollRequest model, CancellationToken cancellationToken)
        {
            var poll = await GetByIdOrThrowException(pollId);

            Mapping(model, poll);

            await _unitOfWork.PollRepository.UpdateAsync(poll);
            await _unitOfWork.SaveAsync(cancellationToken);

            _loggerService.LogInfo($"Poll '{poll.Title}' Updated successfully.");
        }

        public async Task DeletePoll(int id, CancellationToken cancellationToken)
        {
            var poll = await GetByIdOrThrowException(id, cancellationToken);

            await _unitOfWork.PollRepository.DeleteAsync(poll);
            await _unitOfWork.SaveAsync(cancellationToken);

            _loggerService.LogInfo($"Poll '{poll.Title}' deleted successfully.");
        }

        private void CheckValidateDate(CreatePollRequest pollDto)
        {
            if (pollDto.StartsAt.CompareTo(pollDto.EndsAt) > 0 || pollDto.StartsAt.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0 || pollDto.EndsAt.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0)
            {
                _loggerService.LogError("Poll start/end date is invalid.");
                throw new InvalidOperation("Here Mistake in StartsDate or EndsDate");
            }
        }

        private void Mapping(CreatePollRequest model,Poll poll)
        {
            poll.Title = model.Title;
            poll.Description = model.Description;
            poll.IsPublished = model.IsPublished;
            poll.StartsAt = model.StartsAt;
            poll.EndsAt = model.EndsAt;
        }

        private async Task<Poll> GetByIdOrThrowException(int id,CancellationToken cancellationToken = default)
        {
            var Poll = await _unitOfWork.PollRepository.GetByIdAsync(id,cancellationToken);

            if (Poll is null)
            {
                _loggerService.LogError("Poll with #{id} not found.", id);
                throw new ItemNotFound($"Not Found Poll with #Id {id}");
            }

            return Poll;
        }

        private async Task<bool> CheckPollExist(CreatePollRequest pollDto,CancellationToken cancellationToken = default)
        {
            var spec = new PollSpecification(x => x.Title == pollDto.Title && x.Description == pollDto.Description);
           
            var checkExist = await _unitOfWork.PollRepository.GetByIdAsync(spec, cancellationToken);

            return checkExist != null;
        }
        
    }
}
