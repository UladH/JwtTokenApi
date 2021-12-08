namespace Domain.Contracts.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTimeOffset Expiration { get; set; }
    }
}
