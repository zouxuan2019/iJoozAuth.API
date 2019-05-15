using System.Threading.Tasks;
using iJoozAuth.API.Persistence.Contexts;
using IdentityModel;
using IdentityServer4.Validation;

namespace iJoozAuth.API.Service
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public CustomResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
//            if (_userRepository.ValidateCredentialsAsync(context.UserName, context.Password).GetAwaiter().GetResult())
//            {
//                var user = _userRepository.FindByUsernameAsync(context.UserName).GetAwaiter().GetResult();
                context.Result =
                    new GrantValidationResult(context.UserName, OidcConstants.AuthenticationMethods.Password);
//            }

            return Task.FromResult(0);
        }
    }
}