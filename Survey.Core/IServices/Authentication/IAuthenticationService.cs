namespace Survey.Core.IServices.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse?> Login(LoginRequest request, CancellationToken cancellation = default);
        Task Register(RegisterRequest request, CancellationToken cancellation = default);
        Task ConfirmEmailAsync(ConfirmEmailRequest request);
        Task ResendConfirmationEmail(ResendConfirmationEmail resendConfirmationEmail);
        Task SendResetPasswordCodeAsync(string email);
        Task ResetPassword(ResetPasswordRequest request);
        Task<bool> IsEmailExist(string email);
    }
}

