
namespace Survey.Core.Dtos.Poll.Base
{
    public class BasePoll
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
    }
}
