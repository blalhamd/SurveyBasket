
namespace Survey.API.Extensions
{
    public static class Handler
    {
        public static IServiceCollection HandlerConfig(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
