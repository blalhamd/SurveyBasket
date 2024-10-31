namespace Survey.Core.IServices.caching
{
    public interface ICacheService
    {
        Task<string> GetCachedResponseAsync(string cacheKey);
        Task SetResponse(string cacheKey, object response, TimeSpan time);
    }
}
