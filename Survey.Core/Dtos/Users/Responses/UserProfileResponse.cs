namespace Survey.Core.Dtos.Users.Responses
{
    public class UserProfileResponse
    {
        public int Id { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
