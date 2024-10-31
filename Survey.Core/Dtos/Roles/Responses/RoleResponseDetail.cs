namespace Survey.Core.Dtos.Roles.Responses
{
    public class RoleResponseDetail : RoleResponse
    {
        public IEnumerable<string> permissions { get; set; } = null!;
    }
}
