namespace UserSecurityManagerProjectApi.Models
{
    public class JwtConfig
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
    }
}
