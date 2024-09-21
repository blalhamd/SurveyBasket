


namespace Survey.DataAccess.Repositories.Non_Generic
{
    public class PollRepositoryAsync : GenericRepositoryAsync<Poll>, IPollRepositoryAsync
    {
        public PollRepositoryAsync(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
