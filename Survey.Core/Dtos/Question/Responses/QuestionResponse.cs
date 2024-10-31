
namespace Survey.Core.Dtos.Question.Responses
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public List<AnswerResponse> answers { get; set; } = new List<AnswerResponse>();
    }
}
