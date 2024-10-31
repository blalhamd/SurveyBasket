namespace Survey.DataAccess.FluentAPI
{
    public class VoteConfig : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("Votes").HasKey(x => x.Id);

            builder.HasIndex(x => new { x.PollId, x.UserId }).IsUnique();
        }
    }
}


