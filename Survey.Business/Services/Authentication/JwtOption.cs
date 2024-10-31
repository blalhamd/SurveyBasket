
namespace Survey.Business.Services.Authentication
{
    public class JwtOption
    {
        [Required]
        public string Issuer { get; set; }
        [Required]
        public string Audience { get; set; }
        [Range(1, int.MaxValue)]
        public int lifeTime { get; set; }
        [Required]
        public string Key { get; set; }

    }
}
