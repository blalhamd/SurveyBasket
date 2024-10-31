namespace Survey.Core.Dtos.Users.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsDisabled { get; set; }
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
