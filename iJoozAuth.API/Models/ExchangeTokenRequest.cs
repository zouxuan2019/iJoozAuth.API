using System.ComponentModel.DataAnnotations;

namespace iJoozAuth.API.Models
{
    public class ExchangeTokenRequest
    {
        [Required] public string Source { get; set; }
        public string Token { get; set; }
    }
}