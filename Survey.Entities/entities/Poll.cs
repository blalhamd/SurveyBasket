namespace Survey.Entities.entities
{
    public class Poll : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPublished { get; set; }
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
