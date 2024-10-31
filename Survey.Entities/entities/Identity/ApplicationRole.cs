namespace Survey.Entities.entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
    }
}
