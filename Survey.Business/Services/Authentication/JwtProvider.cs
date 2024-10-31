namespace Survey.Business.Services.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOption _JwtOption;
        public JwtProvider(IOptions<JwtOption> jwtOption)
        {
            _JwtOption = jwtOption.Value;
        }
        public ProviderResponse GenerateToken(ApplicationUser user,IEnumerable<string> Roles,IEnumerable<string> permissions)
        {
            var discriptor = new SecurityTokenDescriptor()
            {
                Issuer = _JwtOption.Issuer,
                Audience = _JwtOption.Audience,
                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_JwtOption.lifeTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOption.Key)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Fname),
                    new Claim(ClaimTypes.Email,user.Email),
                    //new Claim(nameof(Roles),JsonSerializer.Serialize(Roles),JsonClaimValueTypes.JsonArray),
                    //new Claim(nameof(permissions),JsonSerializer.Serialize(permissions),JsonClaimValueTypes.JsonArray),
                })
            };

            // another way
            
            foreach (var role in Roles)
            {
                discriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            foreach (var permission in permissions)
            {
                discriptor.Subject.AddClaim(new Claim(Permissions.Type, permission));
            }

            var TokenHandler = new JwtSecurityTokenHandler();
            var Createtoken = TokenHandler.CreateToken(discriptor);
            var Token = TokenHandler.WriteToken(Createtoken);

            return new ProviderResponse { Token = Token, ExprieIn = Convert.ToInt32(_JwtOption.lifeTime) };
        }
    }
}
