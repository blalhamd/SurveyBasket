namespace Survey.API.Controllers
{
    [Authorize(Roles = DefaultRole.Admin)]
    [EnableRateLimiting(policyName: RateLimiterType.Concurrency)]
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Cached(duration: 600)]
        [HttpGet("GellAll")]
        public async Task<ActionResult<IList<UserResponse>>> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [Cached(duration: 600)]
        [HttpGet("GetUserDetail/{userId}")]
        public async Task<ActionResult<UserResponse>> GetUserDetail(int userId)
        {
            return Ok(await _userService.GetUserDetail(userId));
        }

        [HttpPost]
        public async Task<UserResponse> CreateUser(CreateUserRequest request)
        {
            return await _userService.CreateUser(request);
        }

        [HttpPut]
        public async Task<UserResponse> UpdateUser(int id, UpdateUserRequest request)
        {
            return await _userService.UpdateUser(id, request);
        }

        [HttpDelete("{id}/toggle-status")]
        public async Task ToggleUser(int id)
        {
            await _userService.ToggleUser(id);
        }

        [HttpPut("{id}/unlock-user")]
        public async Task UnLockUser(int id)
        {
            await _userService.UnLockUser(id);
        }
    }
}
