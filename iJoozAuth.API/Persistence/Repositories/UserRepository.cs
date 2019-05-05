using System;
using System.Threading.Tasks;
using iJoozAuth.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iJoozAuth.API.Persistence.Contexts
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public UserRepository(UserManager<ApplicationUser> usermanager, AppDbContext context)
        {
            _context = context;
            _usermanager = usermanager;
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await FindByUsernameAsync(username);
            if (user == null)
            {
                return false;
            }

            return await _usermanager.CheckPasswordAsync(user, password);
        }

        public async Task<ApplicationUser> FindByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x =>
                x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}