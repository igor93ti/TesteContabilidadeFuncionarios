namespace ContabilidadeFuncionarios.API.Configurations
{
    public class JwtSettings(string Key, string Issuer, string Audience, int ExpiryMinutes)
    {
        public string Key { get; } = Key;
        public string Issuer { get; } = Issuer;
        public string Audience { get; } = Audience;
        public int ExpiryMinutes { get; } = ExpiryMinutes;
    }
}
