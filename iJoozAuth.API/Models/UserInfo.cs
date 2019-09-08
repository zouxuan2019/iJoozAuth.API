using System.Collections.Generic;

namespace iJoozAuth.API.Models
{
    public class UserInfo
    {
        public string application { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public long expires_at { get; set; }
        public List<string> scopes { get; set; }
    }
}