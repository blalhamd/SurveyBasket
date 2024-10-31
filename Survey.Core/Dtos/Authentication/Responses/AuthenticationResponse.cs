namespace Survey.Core.Dtos.Authentication.Responses
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public int ExpireIn { get; set; }
    }
}
