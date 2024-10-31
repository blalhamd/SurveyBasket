namespace Survey.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleException(ex, context);
                
            }
        }

        private async Task HandleException(Exception ex, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var ErrorResponse = new CustomeErrorResponse()
            {
                Message = ex.Message,
            };

            switch (ex)
            {
                case ItemNotFound:
                    ErrorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    break; 
                case BadRequest:
                    ErrorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ItemAlreadyExist:
                    ErrorResponse.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                case InvalidOperation:
                    ErrorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                    default:
                    break;
            }

            var json = JsonSerializer.Serialize(ErrorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
