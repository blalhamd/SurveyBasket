namespace Survey.Business.Services.Authentication
{
    public class JwtOption
    {
        [Required]
        public string Issuer { get; set; } = null!;
       
        [Required]
        public string Audience { get; set; } = null!;
        
        [Range(1, int.MaxValue)]
        public int lifeTime { get; set; }
        
        [Required]
        public string Key { get; set; } = null!;

    }
}
