using iJoozAuth.API.Models;

namespace iJoozAuth.API.Service
{
    public class JwtTokenGenerator
    {
        public string GenerateJwtToken(UserInfo userInfo)
        {
            return "jwt";
        }
    }
}