using iJoozAuth.API.Persistence.Contexts;
using iJoozAuth.API.Service;
using Microsoft.Extensions.DependencyInjection;

namespace iJoozAuth.API.UserServices

{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.AddProfileService<CustomProfileService>();
            builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}