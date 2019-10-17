namespace iJoozAuth.API.Models
{
    public class FacebookRefreshTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public bool expires_in { get; set; }
    }
}