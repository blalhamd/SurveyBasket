namespace Survey.API.Filters
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _duration;

        public CachedAttribute(int duration)
        {
            _duration = duration;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKey(context.HttpContext.Request);

            var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if(cachedResponse is not null)
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;

                return;
            }
            else
            {
                var executedResultContext = await next();

                if(executedResultContext.Result is OkObjectResult okObjectResult)
                {
                    await cacheService.SetResponse(cacheKey, okObjectResult.Value!, TimeSpan.FromSeconds(_duration));
                }
            }

        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);

            if(request.Query.Count > 0)
            {
                foreach (var (key,value) in request.Query.OrderBy(p=>p.Key))
                {
                    keyBuilder.Append($"|{key}-{value}");
                }
            }

            return keyBuilder.ToString();
        }
    }
}
