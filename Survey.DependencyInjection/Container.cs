namespace Survey.DependencyInjection
{
    public static class Container
    {
        public static async Task<IServiceCollection> AddDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            // Register ConnectionString

            services.RegisterConnectionString(configuration);


            // Register Redis Config
            
            services.RegisterRedisConfig(configuration);
           

            // Register Generic Repos

            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));


            // Register UnitOf Work

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Services 

            services.RegisterServices();

            // Register Identity Config Options

            services.ConfigureIdentityOptions();

            // Register AutoMapper

            services.AddAutoMapper(typeof(Mapping));

            services.AddHttpContextAccessor();

            // Register Concurrency Rate Limiting..

            services.RegisterConcurrencyRateLimitingConfig();

            // Register Token Bucket Rate Limiting..

            services.RegisterTokenBucketLimitingConfig();

            // Register Fixed Window Rate Limiting..

            services.RegisterFixedWindowRateLimiting();

            // Register Sliding Window Rate Limiting..

            services.RegisterSlidingWindowRateLimiting();

            // Register Id Address Rate Limiting..

            services.RegisterIPAddressLimiting();

            // Register User Rate Limiting..

            services.RegisterUserLimiting();

            // await services.SendDefaultData();

            return services;
        }

        public static IServiceCollection RegisterConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration["ConnectionStrings:DefaultConnectionString"];
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connection));
            services.AddScoped<AppDbContext>();

            var connection2 = configuration["ConnectionStrings:SurveyManagentSecurity"];
            services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlServer(connection2));
            services.AddScoped<AppIdentityDbContext>();

            return services;
        }

        public static IServiceCollection RegisterRedisConfig(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IConnectionMultiplexer>(options =>
            {
                var connection = configuration["ConnectionStrings:Redis"];

                return ConnectionMultiplexer.Connect(connection!);
            });

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPollService,PollService>();
            services.AddScoped<IQuestionService,QuestionService>();
            services.AddScoped<IAnswerService,AnswerService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }

        public static async Task<IServiceCollection> SendDefaultData(this IServiceCollection services)
        {
            var scope = services.BuildServiceProvider().CreateScope();
            var provider = scope.ServiceProvider;
            var dbcontext = provider.GetRequiredService<AppDbContext>();
            await SeedData.Seeding(dbcontext);

            return services;
        }

        // this config belong Identity, in case you want to change default values, I mean you can ignore it and will use default values.
        public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
        {
          
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8; // default = 6
                options.SignIn.RequireConfirmedEmail = true; 
            });

            return services;
        }

        private static IServiceCollection RegisterConcurrencyRateLimitingConfig(this IServiceCollection services)
        {
            services.AddRateLimiter(RateLimiterOptions =>
            {
                RateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                RateLimiterOptions.AddConcurrencyLimiter(RateLimiterType.Concurrency, ConcurrencyLimiterOptions =>
                {
                    ConcurrencyLimiterOptions.PermitLimit = 1000;
                    ConcurrencyLimiterOptions.QueueLimit = 100; // will go to waiting list..
                    ConcurrencyLimiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; // will exist empty place to accept request, will move oldest waited request from Queue to execute..
                });
            });

            return services;
        }

        private static IServiceCollection RegisterTokenBucketLimitingConfig(this IServiceCollection services)
        {
            services.AddRateLimiter(RateLimiterOptions =>
            {
                RateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                RateLimiterOptions.AddTokenBucketLimiter(RateLimiterType.TokenBucket, TokenBucketOptions =>
                {
                    TokenBucketOptions.TokenLimit = 10;
                    TokenBucketOptions.QueueLimit = 5;
                    TokenBucketOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    TokenBucketOptions.AutoReplenishment = true; // التجديد التلقائي
                    TokenBucketOptions.ReplenishmentPeriod = TimeSpan.FromMinutes(2); // generate tokens each 2 min and put it in Bucket..
                    TokenBucketOptions.TokensPerPeriod = 5;
                });
            });

            return services;
        }

        private static IServiceCollection RegisterFixedWindowRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(RateLimiterOptions =>
            {
                RateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                RateLimiterOptions.AddFixedWindowLimiter(RateLimiterType.FixedWindow, FixedWindowOptions =>
                {
                    FixedWindowOptions.PermitLimit = 10;
                    FixedWindowOptions.QueueLimit = 10;
                    FixedWindowOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    FixedWindowOptions.Window = TimeSpan.FromMinutes(1);
                    FixedWindowOptions.AutoReplenishment = true;
                });
            });

            return services;
        }

        private static IServiceCollection RegisterSlidingWindowRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(RateLimiterOptions =>
            {
                RateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                RateLimiterOptions.AddSlidingWindowLimiter(RateLimiterType.SlidingWindow, SlidingWindowOptions =>
                {
                    SlidingWindowOptions.PermitLimit = 10;
                    SlidingWindowOptions.QueueLimit = 10;
                    SlidingWindowOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    SlidingWindowOptions.SegmentsPerWindow = 3;
                    SlidingWindowOptions.AutoReplenishment = true;
                    SlidingWindowOptions.Window = TimeSpan.FromMinutes(2);
                });
            });
            
            return services;
        }

        private static IServiceCollection RegisterIPAddressLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(RateLimiterOptions =>
            {
                RateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                RateLimiterOptions.AddPolicy(RateLimiterType.IpLimiting, httpcontext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpcontext.Connection.RemoteIpAddress?.ToString(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 3,
                            Window = TimeSpan.FromSeconds(20) // to wait 20 s to enable from sending another requests..
                        }
                        ));
            });

            return services;
        }

        private static IServiceCollection RegisterUserLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(RateLimiterOptions =>
            {
                RateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                RateLimiterOptions.AddPolicy(RateLimiterType.UserLimiting, httpcontext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpcontext.User?.Identity?.Name?.ToString(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 3,
                            Window = TimeSpan.FromSeconds(20) // to wait 20 s to enable from sending another requests..
                        }
                        ));
            });

            return services;
        }
    }
}
