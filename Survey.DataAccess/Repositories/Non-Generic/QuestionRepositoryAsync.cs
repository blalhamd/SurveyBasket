namespace Survey.DataAccess.Repositories.Non_Generic
{
    public class QuestionRepositoryAsync : GenericRepositoryAsync<Question>, IQuestionRepositoryAsync
    {
        private readonly AppDbContext _appDbContext;
        public QuestionRepositoryAsync(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<QuestionResponse>> GetAllQuestions(int pollId,CancellationToken cancellationToken = default)
        {

            var query = await _appDbContext.Questions.Where(x => x.PollId == pollId)
                                                     .Include(x => x.Answers)
                                                     .Select(x => new QuestionResponse
                                                     {
                                                         Id = x.Id,
                                                         Content = x.Content,
                                                         answers = x.Answers.Select(a => new AnswerResponse()
                                                         {
                                                             Id = a.Id,
                                                             Content = a.Content,
                                                         })
                                                         .ToList(),
                                                     })
                                                     .AsNoTracking()
                                                     .ToListAsync();


            return query;
        }

        public async Task<QuestionResponse> GetQuestionById(int pollId, int id, CancellationToken cancellationToken = default)
        {

            var query = await _appDbContext.Questions.Where(x => x.PollId == pollId && x.Id == id)
                                                     .Include(x => x.Answers)
                                                     .Select(x => new QuestionResponse
                                                     {
                                                         Id = x.Id,
                                                         Content = x.Content,
                                                         answers = x.Answers.Select(a => new AnswerResponse()
                                                         {
                                                             Id = a.Id,
                                                             Content = a.Content,
                                                         })
                                                         .ToList(),
                                                     })
                                                     .AsNoTracking()
                                                     .FirstOrDefaultAsync();

            return query;
        }


        public async Task<IList<QuestionResponse>> GetAvailableAsync(int pollId, int userId, CancellationToken cancellationToken = default)
        {
            var vote = await _appDbContext.Votes.FirstOrDefaultAsync(x=> x.PollId == pollId && x.UserId == userId,cancellationToken);

            if (vote is not null)
                throw new ItemNotFound("Duplicated Vote");

            var polls = await _appDbContext.polls.Where(x => x.Id == pollId &&
                                                      x.IsPublished &&
                                                      x.StartsAt < DateOnly.FromDateTime(DateTime.Now) &&
                                                      x.EndsAt > DateOnly.FromDateTime(DateTime.Now))
                                           .ToListAsync(cancellationToken);

            if (polls is null)
                throw new ItemNotFound("Not Exist Polls");

            var questions = await _appDbContext.Questions.Where(q => q.IsActive && q.PollId == pollId)
                                                         .Include(a => a.Answers)
                                                         .Select(q => new QuestionResponse()
                                                         {
                                                             Id = q.Id,
                                                             Content = q.Content,
                                                             answers = q.Answers.Where(a => a.IsActive)
                                                                                .Select(a => new AnswerResponse()
                                                                                {
                                                                                    Id = a.Id,
                                                                                    Content = a.Content,
                                                                                }).ToList()
                                                         })
                                                         .ToListAsync(cancellationToken);

            return questions;
        }
    }
}
