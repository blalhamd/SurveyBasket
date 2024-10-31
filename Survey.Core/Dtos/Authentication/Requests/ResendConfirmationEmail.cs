namespace Survey.Core.Dtos.Authentication.Requests
{
    public class ResendConfirmationEmail
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
