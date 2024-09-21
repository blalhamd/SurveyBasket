
namespace Survey.Business.Services
{
    public class PollService : IPollService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PollService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<PollResponse>> GetAll()
        {
            var spec = new PollSpecification();
            spec.Includes = [];

            var polls = await _unitOfWork.PollRepository.GetAllAsync(spec);

            if (polls is null)
                throw new Exception();

            var pollsDtos = _mapper.Map<IList<PollResponse>>(polls);

            return pollsDtos;
        }

        public async Task<PollResponse> GetById(int id)
        {
            var spec = new PollSpecification();
            spec.Includes = [];
            spec.Predicate = (x => x.Id == id);

            var poll = await _unitOfWork.PollRepository.GetByIdAsync(spec);

            if (poll is null)
                throw new Exception();

            var pollsDto = _mapper.Map<PollResponse>(poll);

            return pollsDto;
        }


        public async Task AddPoll(CreatePollRequest pollDto)
        {
            if (pollDto is null)
                throw new ArgumentNullException(); // chage as bad req

            var spec = new PollSpecification();
            spec.Predicate = (x => x.Title == pollDto.Title && x.Description == pollDto.Description);

            var checkExist = await _unitOfWork.PollRepository.GetByIdAsync(spec);
           
            if(checkExist is not null)
                throw new ArgumentNullException(); // chage as alrwady exist

            var Poll = _mapper.Map<Poll>(pollDto);

            await _unitOfWork.PollRepository.AddAsync(Poll);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePoll(int pollId, CreatePollRequest poll)
        {
            var Poll = await _unitOfWork.PollRepository.GetByIdAsync(pollId);    

            if (Poll is null)
                throw new ArgumentNullException(); // chage as Not found

            Poll.Title = poll.Title;
            Poll.Description = poll.Description;

            await _unitOfWork.PollRepository.UpdateAsync(Poll);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePoll(int id)
        {
            var Poll = await _unitOfWork.PollRepository.GetByIdAsync(id);

            if (Poll is null)
                throw new ArgumentNullException(); // chage as Not found

            await _unitOfWork.PollRepository.DeleteAsync(Poll);
            await _unitOfWork.SaveAsync();
        }

       
    }
}
