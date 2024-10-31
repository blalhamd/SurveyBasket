namespace Survey.Core.Dtos.Authentication.Requests
{
    public class ConfirmEmailRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Code { get; set; } = null!; // represents Token
    }
}
