namespace Survey.DataAccess.FluentAPI
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions").HasKey(x => x.Id);

            builder.HasIndex(x => new { x.PollId, x.Content }).IsUnique();

            builder.HasOne(x => x.Poll)
                   .WithMany(x => x.Questions)
                   .HasForeignKey(x => x.PollId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    } 
}
