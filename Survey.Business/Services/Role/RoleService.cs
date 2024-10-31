using System.Data;

namespace Survey.Business.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<List<RoleResponse>> GetRolesAsync(bool? IncludeDisabledRoles = false, CancellationToken cancellationToken = default)
        {
            var roles = await _roleManager.Roles.Where(x=> !x.IsDefault && x.IsDeleted == IncludeDisabledRoles).ToListAsync(cancellationToken);

            if (roles is null)
                throw new ItemNotFound("Not Exist Roles");

            var roleResponses = _mapper.Map<List<RoleResponse>>(roles);

            return roleResponses;
        }

        public async Task<RoleResponseDetail> GetRoleByIdAsync(int roleId, CancellationToken cancellationToken = default)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId,cancellationToken);

            if (role is null)
                throw new ItemNotFound("Not Exist Role with this id");

            var permissions = await _roleManager.GetClaimsAsync(role);
            
            var roleResponseDetail = _mapper.Map<RoleResponseDetail>(role);
            roleResponseDetail.permissions = permissions.Select(x=> x.Value).ToList();

            return roleResponseDetail;
        }

        public async Task<RoleResponseDetail> AddRoleAsync(RoleRequest request)
        {
            if (request is null)
                throw new BadRequest("Data is null");

            var IsRoleExist = await _roleManager.RoleExistsAsync(request.Name);

            if (IsRoleExist)
                throw new ItemAlreadyExist("Role is already exist");

            var role = new ApplicationRole()
            {
                Name = request.Name,
            };

            var identityResult = await _roleManager.CreateAsync(role);

            if (!identityResult.Succeeded)
            {
                var error = identityResult.Errors.First();
                throw new InvalidOperation($"{error.Description}");
            }

            var availablePermissions = Permissions.GetAllPermissions();

            if (request.permissions.Except(availablePermissions).Any())
                throw new BadRequest("Invalid permissions");

            var claim = new Claim(nameof(request.permissions),JsonSerializer.Serialize(request.permissions), JsonClaimValueTypes.JsonArray);
            var result = await _roleManager.AddClaimAsync(role, claim);

            if(!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault();
                throw new InvalidOperation($"{error!.Description}");
            }

            return new RoleResponseDetail
            {
                Id = role.Id,
                Name = request.Name,
                permissions = request.permissions,
                IsDeleted = false,
            };
        }

        public async Task<RoleResponseDetail> UpdateRoleAsync(int roleId, RoleRequest request)
        {
            if (request is null)
                throw new BadRequest("Data is null");

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r=> r.Id == roleId);

            if (role is null)
                throw new ItemNotFound("Role is not found");

            role.Name = request.Name;

            var availablePermissions = Permissions.GetAllPermissions();

            if (request.permissions.Except(availablePermissions).Any())
                throw new BadRequest("Invalid permissions");

            var claim = new Claim(nameof(request.permissions), JsonSerializer.Serialize(request.permissions), JsonClaimValueTypes.JsonArray);
            var result = await _roleManager.AddClaimAsync(role, claim);

            var identityResult = await _roleManager.UpdateAsync(role);

            if (!identityResult.Succeeded)
            {
                var error = identityResult.Errors.First();
                throw new InvalidOperation($"{error.Description}");
            }

            return new RoleResponseDetail
            {
                Id = role.Id,
                Name = request.Name,
                IsDeleted = false,
                permissions = request.permissions
            };
        }
        public async Task<bool> ToggleRoleAsync(int roleId)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (role is null)
                throw new ItemNotFound("Role is not found");

            role.IsDeleted = !role.IsDeleted;

            var identityResult = await _roleManager.UpdateAsync(role);

            if (!identityResult.Succeeded)
            {
                var error = identityResult.Errors.First();
                throw new InvalidOperation($"{error.Description}");
            }

            return true;
        }

    }
}
