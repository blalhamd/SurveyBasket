namespace Survey.Core.Dtos.Users.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; } = null!;

        [Required]
        [RegularExpression(RegxPattern.PasswordPattern, ErrorMessage = "Password must have Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.")]
        [CheckPassword]
        public string NewPassword { get; set; } = null!;
    }
}
