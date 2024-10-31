namespace Survey.Core.Dtos.Authentication.Requests
{
    public class ForgetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
