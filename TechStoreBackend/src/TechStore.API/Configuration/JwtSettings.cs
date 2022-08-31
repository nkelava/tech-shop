

namespace TechStore.API.Configuration
{
    public class JwtSettings
    {
        public string SecretKey { get; init; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public TimeSpan ExpiryTimeFrame { get; set; }
    }
}
