namespace App.Contracts.Models
{
    public class TokenPairModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
