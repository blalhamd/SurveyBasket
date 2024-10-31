namespace Survey.Core.Dtos.Users.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        public string Fname { get; set; } = null!;

        [Required]
        public string Lname { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [RoleRequest]
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
