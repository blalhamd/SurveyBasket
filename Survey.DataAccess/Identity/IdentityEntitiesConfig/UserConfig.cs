namespace Survey.DataAccess.Identity.IdentityEntitiesConfig
{
    public class UserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers").HasKey(t => t.Id);

            builder.HasData(LoadUser());
        }

        private ApplicationUser LoadUser()
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            return new ApplicationUser
            {
                Id = DefaultUser.AdminId,
                Email = DefaultUser.AdminEmail,
                NormalizedEmail = DefaultUser.AdminEmail.ToUpper(),
                EmailConfirmed = true, // must be true
                Fname = "Bilal",
                Lname = "El Sayed",
                UserName = DefaultUser.AdminEmail.Split('@').First(), // Admin48
                NormalizedUserName = DefaultUser.AdminEmail.Split('@').First().ToUpper(),
                SecurityStamp = DefaultUser.SecurityStamp,
                ConcurrencyStamp = DefaultUser.ConcurrencyStamp,
                PasswordHash = passwordHasher.HashPassword(null!,DefaultUser.AdminPassword),
            };
        }
    }
}
