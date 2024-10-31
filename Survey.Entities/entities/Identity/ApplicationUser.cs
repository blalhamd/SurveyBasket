namespace Survey.Entities.entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public bool IsDisabled { get; set; }
    }
}
