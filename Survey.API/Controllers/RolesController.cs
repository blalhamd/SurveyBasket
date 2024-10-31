namespace Survey.API.Controllers
{
    [EnableRateLimiting(policyName: RateLimiterType.Concurrency)]
    public class RolesController : ApiBaseController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Cached(duration: 600)]
        [HttpGet]
        [Authorize(Roles = DefaultRole.Admin)]
        public async Task<ActionResult<List<RoleResponse>>> GetRolesAsync([FromQuery] bool? IncludeDisabledRoles = false, CancellationToken cancellationToken = default)
        {
            return Ok(await _roleService.GetRolesAsync(IncludeDisabledRoles, cancellationToken));
        }

        [Cached(duration: 600)]
        [HttpGet("{roleId}")]
        [Authorize(Roles = DefaultRole.Admin)]
        public async Task<ActionResult<RoleResponseDetail>> GetRoleByIdAsync(int roleId,CancellationToken cancellationToken = default)
        {
            return Ok(await _roleService.GetRoleByIdAsync(roleId, cancellationToken));
        }

        [HttpPost]
        [Authorize(Roles = DefaultRole.Admin)]
        public async Task<RoleResponseDetail> AddRoleAsync(RoleRequest request)
        {
            return await _roleService.AddRoleAsync(request);
        }
    }
}
