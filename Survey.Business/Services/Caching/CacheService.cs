namespace Survey.Business.Services.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer multiplexer)
        {
            _database = multiplexer.GetDatabase();
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);

            if (cachedResponse.IsNullOrEmpty)
                return null!;

            return cachedResponse!;
        }

        public async Task SetResponse(string cacheKey, object response, TimeSpan time)
        {
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await _database.StringSetAsync(cacheKey, JsonSerializer.Serialize(response, options), time);
        }
    }
}
