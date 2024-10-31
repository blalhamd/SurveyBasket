namespace Survey.Core.Dtos.Roles.Responses
{
    public class RoleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
