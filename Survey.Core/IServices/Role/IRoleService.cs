namespace Survey.Core.IServices.Role
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> GetRolesAsync(bool? IncludeDisabledRoles = false,CancellationToken cancellationToken = default);
        Task<RoleResponseDetail> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken = default);
        Task<RoleResponseDetail> AddRoleAsync(RoleRequest request);
        Task<RoleResponseDetail> UpdateRoleAsync(int roleId, RoleRequest request);
        Task<bool> ToggleRoleAsync(int roleId);
    }
}
