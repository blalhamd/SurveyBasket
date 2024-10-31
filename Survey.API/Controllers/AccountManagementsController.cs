namespace Survey.API.Controllers
{
    [EnableRateLimiting(policyName: RateLimiterType.Concurrency)]
    public class AccountManagementsController : ApiBaseController
    {
        private readonly IUserService _userService;

        public AccountManagementsController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<UserProfileResponse> GetUserProfileResponse()
        {
            var userId = User.GetUserId();
            return await _userService.GetUserProfile(Convert.ToInt32(userId));
        }

        [Authorize]
        [HttpPut]
        public async Task<UserProfileResponse> UpdateUserProfile(UpdateUserProfileRequest userProfileRequest)
        {
            var userId = User.GetUserId();

            return await _userService.UpdateUserProfile(Convert.ToInt32(userId), userProfileRequest);
        }

        [Authorize]
        [HttpPut("Change-Password")]
        public async Task<bool> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var userId = User.GetUserId();

            return await _userService.ChangePassword(Convert.ToInt32(userId), changePasswordRequest);
        }

    }
}
