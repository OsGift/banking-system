namespace BankingSystem.Infrastructure.Configurations
{
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }

    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string JwtSecret { get; set; } = string.Empty;
    }

    public class RedisSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public int DefaultCacheDurationInMinutes { get; set; }
    }
}
