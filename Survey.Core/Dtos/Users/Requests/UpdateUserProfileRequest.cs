namespace Survey.Core.Dtos.Users.Requests
{
    public class UpdateUserProfileRequest
    {
        [Required]
        public string Fname { get; set; } = null!;
       
        [Required]
        public string Lname { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
