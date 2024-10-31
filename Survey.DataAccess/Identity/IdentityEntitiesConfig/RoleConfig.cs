namespace Survey.DataAccess.Identity.IdentityEntitiesConfig
{
    public class RoleConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("AspNetRoles").HasKey(t => t.Id);

            builder.HasData(LoadRole());
        }

        private IEnumerable<ApplicationRole> LoadRole()
        {
            return new List<ApplicationRole>()
            {
                 new ApplicationRole()
                {
                   Id = DefaultRole.AdminRoleId,
                   Name = DefaultRole.Admin,
                   NormalizedName = DefaultRole.Admin.ToUpper(),
                   ConcurrencyStamp = DefaultRole.AdminConcurrencyStamp,
                   IsDefault = false,
                   IsDeleted = false,
                },
                 new ApplicationRole()
                {
                   Id = DefaultRole.MemberRoleId,
                   Name = DefaultRole.Member,
                   NormalizedName = DefaultRole.Member.ToUpper(),
                   ConcurrencyStamp = DefaultRole.MemberConcurrencyStamp,
                   IsDefault = true,
                   IsDeleted = false,
                }
            };
        }

    }
}
