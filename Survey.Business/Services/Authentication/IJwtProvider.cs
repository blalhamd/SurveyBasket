namespace Survey.Business.Services.Authentication
{
    public interface IJwtProvider
    {
        ProviderResponse GenerateToken(ApplicationUser user, IEnumerable<string> Roles, IEnumerable<string> permissions);
    }
}
