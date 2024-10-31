
namespace Survey.DataAccess.FluentAPI
{
    public class AnswerConfig : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers").HasKey(x => x.Id);

            builder.HasIndex(x => new { x.QuestionId, x.Content }).IsUnique();
        }
    }
}


