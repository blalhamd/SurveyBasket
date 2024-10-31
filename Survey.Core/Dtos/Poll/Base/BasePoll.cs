namespace Survey.Core.Dtos.Poll.Base
{
    public class BasePoll
    {
        [Required]
        [MinLength(10)]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPublished { get; set; }
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }

    }
}
