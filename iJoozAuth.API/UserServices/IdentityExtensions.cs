using iJoozAuth.API.Service;
using Microsoft.Extensions.DependencyInjection;

namespace iJoozAuth.API.UserServices

{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();
            return builder;
        }
    }
}