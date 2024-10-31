namespace Survey.API.Extensions
{
    public static class ValidationErrorResponseExtenstion
    {
        public static IServiceCollection AddConfigure(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                                                   .SelectMany(P => P.Value.Errors)
                                                   .Select(x => x.ErrorMessage)
                                                   .ToList();

                    var validationError = new ValidationErrorResponse()
                    {
                        Errors = errors,
                    };

                    return new BadRequestObjectResult(validationError);
                };
            });

            return services;
        }
    }
}
