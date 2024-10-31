namespace Survey.Core.Dtos.Authentication.Requests
{
    public class RegisterRequest
    {
        [Required]
        [RegularExpression(RegxPattern.FirstNamePattern,ErrorMessage = "First Name must be letters only.")]
        public string FirstName { get; set; } = null!;
      
        [Required]
        [RegularExpression(RegxPattern.LastNamePattern, ErrorMessage = "Last Name must be letters only.")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(RegxPattern.PasswordPattern,ErrorMessage = "Password must have Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string Password { get; set; } = null!;

    }
}
