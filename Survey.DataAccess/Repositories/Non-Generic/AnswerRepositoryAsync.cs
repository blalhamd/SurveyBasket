namespace Survey.DataAccess.Repositories.Non_Generic
{
    public class AnswerRepositoryAsync : GenericRepositoryAsync<Answer>, IAnswerRepositoryAsync
    {
        public AnswerRepositoryAsync(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
