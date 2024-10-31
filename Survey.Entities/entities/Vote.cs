namespace Survey.Entities.entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public int UserId { get; set; }
        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
        public Poll Poll { get; set; } = null!;
        
        [NotMapped]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<VoteAnswer> voteAnswers { get; set; } = new List<VoteAnswer>();
    }
}
