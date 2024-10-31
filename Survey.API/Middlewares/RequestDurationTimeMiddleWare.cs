namespace Survey.API.Middlewares
{
    public class RequestDurationTimeMiddleWare
    {
        private readonly RequestDelegate _next;
        public RequestDurationTimeMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                await _next(context);
                stopWatch.Stop();
                Console.WriteLine($"Time of Request is {stopWatch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
