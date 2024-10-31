namespace Survey.Core.Dtos.Authentication.Requests
{
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;
       
        [Required]
        [RegularExpression(RegxPattern.PasswordPattern, ErrorMessage = "Password must have Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string NewPassword { get; set; } = null!;
    }
}
