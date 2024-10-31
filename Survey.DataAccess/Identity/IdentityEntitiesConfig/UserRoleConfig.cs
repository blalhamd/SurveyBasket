namespace Survey.DataAccess.Identity.IdentityEntitiesConfig
{
    public class UserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.ToTable("AspNetUserRoles").HasKey(t => new { t.RoleId, t.UserId });

            builder.HasData(LoadUserRole());
        }

        private IEnumerable<IdentityUserRole<int>> LoadUserRole()
        {
            return new List<IdentityUserRole<int>>()
            {
                new IdentityUserRole<int>
                {
                   UserId = DefaultUser.AdminId,
                   RoleId = DefaultRole.AdminRoleId
                },
                new IdentityUserRole<int>
                {
                   UserId = DefaultUser.AdminId,
                   RoleId = DefaultRole.MemberRoleId
                },
            };
        }
    }
}
