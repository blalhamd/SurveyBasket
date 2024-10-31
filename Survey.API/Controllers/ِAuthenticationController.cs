namespace Survey.API.Controllers
{
    [EnableRateLimiting(policyName: RateLimiterType.Concurrency)]
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        [EnableRateLimiting(policyName: RateLimiterType.IpLimiting)]
        public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest, CancellationToken cancellation = default)
        {
            return await _authenticationService.Login(loginRequest, cancellation);
        }

        [HttpPost("Register")]
        [EnableRateLimiting(policyName: RateLimiterType.IpLimiting)]
        public async Task<ActionResult> Register(RegisterRequest request, CancellationToken cancellation = default)
        {
             await _authenticationService.Register(request, cancellation);

            return Ok();
        }

        [HttpGet("CheckEmailExist")]
        public async Task<bool> EmailExist(string email)
        {
            return await _authenticationService.IsEmailExist(email);
        }

        [HttpPost("confirm-email")]
        public async Task ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            await _authenticationService.ConfirmEmailAsync(request);
        }

        [HttpPost("Resend-Confirmation-Email")]
        public async Task ResendConfirmationEmail(ResendConfirmationEmail resendConfirmationEmail)
        {
            await _authenticationService.ResendConfirmationEmail(resendConfirmationEmail);
        }

        [HttpPost("forget-password")]
        public async Task ForgetPassword(ForgetPasswordRequest request)
        {
             await _authenticationService.SendResetPasswordCodeAsync(request.Email);
        }

        [HttpPost("Reset-Password")]
        public async Task ResetPassword(ResetPasswordRequest request)
        {
            await _authenticationService.ResetPassword(request);
        }
    }
}
