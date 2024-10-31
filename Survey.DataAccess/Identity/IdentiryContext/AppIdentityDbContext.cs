namespace Survey.DataAccess.Identity.IdentiryContext
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new RoleConfig());
            builder.ApplyConfiguration(new UserRoleConfig());
            builder.ApplyConfiguration(new RoleClaimConfig());
        }
    }
}

