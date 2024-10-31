namespace Survey.Core.Dtos.Users.Validations
{
    public class CheckPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var changePasswordRequest = (ChangePasswordRequest)validationContext.ObjectInstance;

            if(value!.ToString() == changePasswordRequest.CurrentPassword)
            {
                return new ValidationResult("New Password can't be the same old password");
            }

            return ValidationResult.Success;
        }
    }
}
