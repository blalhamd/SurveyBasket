
namespace Survey.DependencyInjection
{
    public static class Container
    {
        public static async Task<IServiceCollection> AddDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            // Register ConnectionString

            services.RegisterConnectionString(configuration);


            // Register Generic Repos

            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));


            // Register UnitOf Work

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Services 

            services.RegisterServices();

            // Register AutoMapper

            services.AddAutoMapper(typeof(Mapping));

         //   await services.SendDefaultData();

            return services;
        }

        public static IServiceCollection RegisterConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration["ConnectionStrings:DefaultConnectionString"];
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connection));
            services.AddScoped<AppDbContext>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPollService,PollService>();

            return services;
        }

        //public static async Task<IServiceCollection> SendDefaultData(this IServiceCollection services)
        //{
        //    var scope = services.BuildServiceProvider().CreateScope();
        //    var provider = scope.ServiceProvider;
        //    var dbcontext = provider.GetRequiredService<AppDbContext>();
        //    await SeedData.Seeding(dbcontext);

        //    return services;
        //}
    }
}
