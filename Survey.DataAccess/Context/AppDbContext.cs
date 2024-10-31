namespace Survey.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Poll> polls { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteAnswer> VoteAnswers { get; set; }

        public readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers", t => t.ExcludeFromMigrations());

            #region in case you want to make all relations OnDeleteBehavior as Restrict 
          
            var forignKeys = modelBuilder.Model.GetEntityTypes()
                                               .SelectMany(t => t.GetForeignKeys())
                                               .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership)
                                               .ToList();

            foreach (var fk in forignKeys)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            var CurrentUserIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
          
            int? CurrentUserId = null;

            if (CurrentUserIdClaim != null && int.TryParse(CurrentUserIdClaim.Value, out var parsedUserId))
            {
                CurrentUserId = parsedUserId;
            }

            foreach (var entryEntity in entries)
            {
                if (entryEntity is not null && CurrentUserId is not null)
                {
                    if (entryEntity.State is EntityState.Added)
                    {
                        entryEntity.Property(x => x.CreationTime).CurrentValue = DateTime.UtcNow;
                        entryEntity.Property(x => x.CreatedByUserId).CurrentValue = CurrentUserId.Value;
                    }
                    else if (entryEntity.State is EntityState.Modified)
                    {
                        entryEntity.Property(x => x.ModificatedByUserId).CurrentValue = CurrentUserId.Value;

                        if (entryEntity.Property(x => x.FirstModificationDate).CurrentValue is null)
                        {
                            entryEntity.Property(x => x.FirstModificationDate).CurrentValue = DateTime.Now;
                        }
                        else
                        {
                            entryEntity.Property(x => x.LastModificationDate).CurrentValue = DateTime.Now;
                        }
                    }
                    else if (entryEntity.State is EntityState.Deleted && entryEntity.Entity is ISoftDeletable)
                    {
                        entryEntity.State = EntityState.Modified;
                        entryEntity.Property(x => x.IsDeleted).CurrentValue = true;
                        entryEntity.Property(x => x.DeletedAt).CurrentValue = DateTime.Now;
                        entryEntity.Property(x => x.DeletedByUserId).CurrentValue = CurrentUserId.Value;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


    }
}


