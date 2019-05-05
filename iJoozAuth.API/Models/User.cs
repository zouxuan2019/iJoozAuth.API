using System.ComponentModel.DataAnnotations.Schema;

namespace iJoozAuth.API.Models
{
    [Table("AspNetUsers")]
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}