namespace IyiOlusAdminPanel.Models.Login
{
    public class TokenResponse
    {
        public string token { get; set; } = default!;
        public string refreshToken { get; set; } = default!;
        public DateTime expiration { get; set; }
    }
}
