
namespace Survey.Core.Dtos.Question.Validations
{
    public class AnswerValidationAttribute : ValidationAttribute
    {
        public new string ErrorMessage { get; set; } = "Question must have at least 2 answers";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not null)
            {
               var answers = (List<string>)value;
              
                if (answers.Count > 1)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
