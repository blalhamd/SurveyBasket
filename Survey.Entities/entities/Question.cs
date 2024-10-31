namespace Survey.Entities.entities
{
    public class Question : BaseEntity 
    {
        public string Content { get; set; } = null!;
        public int PollId { get; set; }
        public Poll Poll { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<VoteAnswer> voteAnswers { get; set; } = new List<VoteAnswer>();
    }
}
