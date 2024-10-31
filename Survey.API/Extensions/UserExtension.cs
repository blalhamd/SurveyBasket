namespace Survey.API.Extensions
{
    public static class UserExtension
    {
        public static string? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        } 
    }
}
