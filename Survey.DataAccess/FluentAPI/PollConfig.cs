
namespace Survey.DataAccess.FluentAPI
{
    public class PollConfig : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.ToTable("Polls").HasKey(x => x.Id);
        }
    }
}
