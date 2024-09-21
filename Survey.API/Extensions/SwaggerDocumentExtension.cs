namespace Survey.API.Extensions
{
    public static class SwaggerDocumentExtension
    {
        public static IServiceCollection AddSwaggerCongig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
