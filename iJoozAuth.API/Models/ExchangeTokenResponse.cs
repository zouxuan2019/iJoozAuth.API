namespace iJoozAuth.API.Models
{
    public class ExchangeTokenResponse
    {
        public string access_token;
        public long expires_in;
        public string token_type;
        public string refresh_token;
        public string source;
    }
}