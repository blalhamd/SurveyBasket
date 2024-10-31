namespace Survey.DataAccess.Identity.IdentityEntitiesConfig
{
    public class RoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
        {
            builder.ToTable("AspNetRoleClaims").HasKey(t => t.Id);

            builder.HasData(LoadRoleClaims());
        }

        private IEnumerable<IdentityRoleClaim<int>> LoadRoleClaims()
        {
            var permissions = Permissions.GetAllPermissions();
            var AdminClaims = new List<IdentityRoleClaim<int>>();

            for (int i = 0; i < permissions.Count(); i++)
            {
                AdminClaims.Add(new IdentityRoleClaim<int>()
                {
                    Id = i + 1,
                    ClaimType = Permissions.Type,
                    ClaimValue = permissions[i],
                    RoleId = DefaultRole.AdminRoleId,
                });
            }

            return AdminClaims;
        }
    }
}
