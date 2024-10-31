
namespace Survey.Core.Dtos.Question.Requests
{
    public class QuestionRequest
    {
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        [AnswerValidation]
        [AnswerDuplicationValidation]
        public List<string> answers { get; set; } = new List<string>();
    }
}
