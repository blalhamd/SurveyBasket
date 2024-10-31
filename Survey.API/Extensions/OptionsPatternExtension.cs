using Survey.Business.Services.Email;

namespace Survey.API.Extensions
{
    public static class OptionsPatternExtension
    {
        public static IServiceCollection OptionsPatternConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JwtOption>(configuration.GetSection("Jwt"));
            services.AddOptions<JwtOption>(configuration["Jwt"])
                            .ValidateDataAnnotations()
                            .ValidateOnStart();

            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

            return services;
        }
    }
}
