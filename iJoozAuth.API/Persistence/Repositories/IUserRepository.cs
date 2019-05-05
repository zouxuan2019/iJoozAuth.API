using System.Threading.Tasks;
using iJoozAuth.API.Models;

namespace iJoozAuth.API.Persistence.Contexts
{
    public interface IUserRepository
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);

        Task<ApplicationUser> FindByUsernameAsync(string username);
    }
}