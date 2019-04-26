using System.Collections.Generic;
using System.Linq;
using System;

namespace iJoozAuth.API.UserServices
{
    public class UserRepository : IUserRepository
    {
        // some dummy data. Replce this with your user persistence. 
        private readonly List<CustomUser> _users = new List<CustomUser>
        {
            new CustomUser
            {
                SubjectId = "123",
                UserName = "zouxuan",
                Password = "zouxuan",
                Email = "zouxuan@gmail.com"
            },
            new CustomUser
            {
                SubjectId = "124",
                UserName = "zouxuan2",
                Password = "zouxuan22",
                Email = "zouxuan2@gmail.com"
            },
        };

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                return user.Password.Equals(password);
            }

            return false;
        }

        public CustomUser FindBySubjectId(string subjectId)
        {
            return _users.FirstOrDefault(x => x.SubjectId == subjectId);
        }

        public CustomUser FindByUsername(string username)
        {
            return _users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}