namespace Survey.Entities.entities
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public ICollection<VoteAnswer> voteAnswers { get; set; } = new List<VoteAnswer>();
    }
}
