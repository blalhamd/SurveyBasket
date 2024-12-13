namespace Survey.Business.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILoggerService _logger;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthenticationService
            (
            UserManager<ApplicationUser> userManager,
            IJwtProvider jwtProvider,
            SignInManager<ApplicationUser> signInManager,
            ILoggerService logger,
            IEmailSender emailSender,
            IHttpContextAccessor contextAccessor,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _roleManager = roleManager;
        }

        public async Task<AuthenticationResponse?> Login(LoginRequest request, CancellationToken cancellation = default)
        {
            // check email

            var user = await _userManager.FindByEmailAsync(request.Email);

            // check password

            if (user is null)
                throw new BadRequest("Invalid UserName or Password");

            if(user.IsDisabled)
                throw new BadRequest($"Disabled User, please contact to your admin");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return result.IsNotAllowed ?
                            throw new BadRequest("you must confirm email.") :
                            result.IsLockedOut ?
                            throw new BadRequest("Your are Locked Out") :
                            throw new BadRequest("Invalid Email Or Password");

            }

            var (roles,permissions) = await GetUserRolesAndPermissions(user);

            var model = _jwtProvider.GenerateToken(user, roles, permissions);

            var authResponse = new AuthenticationResponse()
            {
                Id = user.Id,
                FirstName = user.Fname,
                LastName = user.Lname,
                Email = user.Email,
                Token = model.Token,
                ExpireIn = model.ExprieIn
            };

            return authResponse is null ? throw new BadRequest("Invalid UserName or Password") : authResponse;
        }

        // Note,Don't forget after studied refresh token section to apply here too on Register endpoint (Explination it in Add Register Endpoint Video in Registeration Section)
        public async Task Register(RegisterRequest request, CancellationToken cancellation = default)
        {
            if (request is null)
                throw new BadRequest("Date is null");

            var checkExistEmail = await IsEmailExist(request.Email);

            if (checkExistEmail)
                throw new ItemAlreadyExist("This Email Already Exist");

            var appUser = new ApplicationUser()
            {
                Fname = request.FirstName,
                Lname = request.LastName,
                Email = request.Email,
                UserName = request.Email.Split('@').First(),
            };

            var CreatedUser = await _userManager.CreateAsync(appUser, request.Password);

            if (!CreatedUser.Succeeded)
            {
                var error = CreatedUser.Errors.FirstOrDefault();
                throw new BadRequest($"Error Code: {error?.Code}\nError Description: {error?.Description}\n");
            }
            
            await SendEmailConfirmation(appUser);
        }

        public async Task ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (request is null)
                throw new BadRequest("Data is null");

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
            {
                throw new ItemNotFound("Invalid Id Or Code");
            }

            if(user.EmailConfirmed)
            {
                throw new BadRequest("Duplicated Email Confirmed");
            }

            request.Code = DecodeCode(request.Code);

            var result = await _userManager.ConfirmEmailAsync(user, request.Code);

            // assign Registered User to default Role after success confirmation email, if you did't apply confirmation email will do this step in register method.

            if(result.Succeeded)
            {
                var identityResult = await _userManager.AddToRoleAsync(user, DefaultRole.Member);

                if (!identityResult.Succeeded)
                {
                    throw new InvalidOperation($"Description: {result.Errors.Select(x=> x.Description).FirstOrDefault()}");
                }

                return;
            }

            var error = result.Errors.FirstOrDefault();

            throw new InvalidOperation($"ErrorCode: {error?.Code}\nDescription: {error?.Description}");
        }

        private string DecodeCode(string code)
        {
            try
            {
                return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException ex)
            {
                throw new BadRequest(ex.Message);
            }
        }

        public async Task ResendConfirmationEmail(ResendConfirmationEmail resendConfirmationEmail)
        {
            var user = await _userManager.FindByEmailAsync(resendConfirmationEmail.Email);

            if(user is not null)
            {
                if (user.EmailConfirmed)
                {
                    throw new InvalidOperation(400, "Email of User is already confirmed");
                }

                await SendEmailConfirmation(user);
            }
            else
            {
                throw new ItemNotFound("Email is not exist");
            }
        }

        private async Task SendEmailConfirmation(ApplicationUser appUser)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            _logger.LogInfo("Confirmation Code: {code}", code);

            var origin = _contextAccessor.HttpContext?.Request.Headers.Origin!;

            var EmailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                new Dictionary<string, string>()
                {
                    {"{{name}}",appUser.Fname },
                    {"{{action_url}}", $"{origin}/Authentication/confirm-email?userId={appUser.Id}&code={code}" }
                }
                );

            await _emailSender.SendEmailAsync(appUser.Email, "Survey Basket: Confirmation Email", EmailBody);
        }

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
                throw new ItemNotFound("code is invalid"); // من باب الخداع

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
                result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch (FormatException)
            {
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
            }

            if (!result.Succeeded)
                throw new InvalidOperation(400, result.Errors.Select(x => x.Description).First());

            return;
        }

        public async Task SendResetPasswordCodeAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return; // من باب الخداع 

            if (!user.EmailConfirmed)
                throw new BadRequest("Confirmation email is required");

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInfo("Reset code: {code}", code);

            await SendResetPasswordEmail(user, code);

            return;
        }

        private async Task SendResetPasswordEmail(ApplicationUser user, string code)
        {
            var origin = _contextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
                new Dictionary<string, string>
                {
                { "{{name}}", user.Fname },
                { "{{action_url}}", $"{origin}/Authentication/forget-password?email={user.Email}&code={code}" }
                }
            );

            await _emailSender.SendEmailAsync(user.Email!, "✅ Survey Basket: Change Password", emailBody);

            await Task.CompletedTask;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user != null ? true : false;
        }

        private async Task<(IEnumerable<string> roles,IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var permissions = new List<string>();

            foreach (var roleName in roles)
            {
                var role = await _roleManager.Roles.FirstOrDefaultAsync(x=> x.Name == roleName);
              
                if(role != null)
                {
                    var claims = await _roleManager.GetClaimsAsync(role);
                    permissions.AddRange(claims.Select(x => x.Value));
                }
            }

            return (roles,permissions);
        } 
    }
}
//private readonly UserManager<ApplicationUser> _userManager;
//private readonly JwtOption _JwtOption;
//public AuthenticationService(UserManager<ApplicationUser> userManager, JwtOption jwtOption)
//{
//    _userManager = userManager;
//    _JwtOption = jwtOption;
//}

//public async Task<AuthenticationResponse?> Login(LoginRequest request)
//{
//    // check email

//    var user = await _userManager.FindByEmailAsync(request.Email);

//    // check password

//    if (user == null)
//         throw new BadRequest("Invalid UserName or Password");

//    var CheckPassword = await _userManager.CheckPasswordAsync(user, request.Password);

//    if (!CheckPassword)
//         throw new BadRequest("Invalid UserName or Password");

//    // generate token
//    var discriptor = new SecurityTokenDescriptor()
//    {
//        Issuer = _JwtOption.Issuer,
//        Audience = _JwtOption.Audience,
//        Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_JwtOption.lifeTime)),
//        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOption.Key)), SecurityAlgorithms.HmacSha256),
//        Subject = new ClaimsIdentity(new List<Claim>
//        {
//            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
//            new Claim(ClaimTypes.Name,user.Fname),
//            new Claim(ClaimTypes.Email,user.Email),
//        })
//    };

//    var TokenHandler = new JwtSecurityTokenHandler();
//    var Createtoken = TokenHandler.CreateToken(discriptor);
//    var Token = TokenHandler.WriteToken(Createtoken);

//    // customzie response


//    var result = new AuthenticationResponse()
//    {
//        Email = request.Email,
//        FirstName = user.Fname,
//        LastName = user.Lname,
//        Token = Token,
//        ExpireIn = Convert.ToInt32(_JwtOption.lifeTime)
//    };

//    return result is null? throw new BadRequest("Invalid UserName or Password") : result; 
//}

//public Task<AuthenticationResponse?> Register(RegisterRequest request)
//{
//    throw new NotImplementedException();
//}
