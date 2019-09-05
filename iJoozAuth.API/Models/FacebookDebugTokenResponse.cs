using System.Collections.Generic;

namespace iJoozAuth.API.Models
{
    public class FacebookDebugTokenResponse
    {
        public Data data { get; set; }
        public class Data
        {
            public string app_id { get;set;  }
            public string application{ get; set;}
            public bool is_valid{ get;set; }
            public long expires_at{ get;set; }
            public string user_id { get; set; }
            public List<string> scopes{ get; set;}
        }
    }
}