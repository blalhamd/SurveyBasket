namespace Survey.Core.Dtos.Roles.Requests
{
    public class RoleRequest
    {
        [Required]
        public string Name { get; set; } = null!;
      
        [Required]
        [RoleRequest]
        public IEnumerable<string> permissions { get; set; } = null!;
    }
}
