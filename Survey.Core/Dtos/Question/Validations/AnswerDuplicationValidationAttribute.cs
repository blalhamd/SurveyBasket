
namespace Survey.Core.Dtos.Question.Validations
{
    public class AnswerDuplicationValidationAttribute : ValidationAttribute
    {
        public new string ErrorMessage { get; set; } = "Answer of Question is Duplicated";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                var answers = (List<string>)value;

                if (answers.Distinct().Count() == answers.Count)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
