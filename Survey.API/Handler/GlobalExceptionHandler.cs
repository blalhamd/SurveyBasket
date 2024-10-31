/*
 * settings in program.cs -->
 * 
 * in middlewares  
 * app.UseExceptionHandler();  &&
 * 
 * in services  
 * builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
 * builder.Services.AddProblemDetails();
 * 
*/
namespace Survey.API.Handler
{
    public class GlobalExceptionHandler : IExceptionHandler 
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            await HandleError(exception, httpContext, cancellationToken);

            return true;
        }

        private async Task HandleError(Exception exception, HttpContext httpContext, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new CustomeErrorResponse()
            {
                Message = exception.Message,
            };

            switch (exception)
            {
                case ItemNotFound:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ItemAlreadyExist:
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                case BadRequest:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case InvalidOperation:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    break;
            }

            var json = JsonSerializer.Serialize(response);

            await httpContext.Response.WriteAsync(json, cancellationToken);
        }
    }
}
