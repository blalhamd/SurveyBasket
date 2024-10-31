namespace Survey.API.Extensions
{
    public static class Cors
    {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(p =>
            {
                p.AddPolicy(PolicyName.AdminPolicy, options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });

                p.AddPolicy(PolicyName.MangerPolicy, options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4201");
                });

                p.AddPolicy(PolicyName.UserPolicy, options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5000");
                });
            });

            return services;
        }

        //public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        //{
        //    services.AddCors(p => p.AddPolicy(PolicyName.AdminPolicy, options => options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200")));

        //    return services;
        //}
    }
}
