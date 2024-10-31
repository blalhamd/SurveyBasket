﻿
namespace Survey.API.Extensions
{
    public static class JwtConfig
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services,IConfiguration configuration)
        {
            var JwtOptions = configuration.GetSection("Jwt").Get<JwtOption>();

            services.AddSingleton(JwtOptions);

            services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
            {
                options.SaveToken = true;

                // here will validate parameters of Token.
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = JwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key))
                };
            });

            return services;
        }
    }
}
