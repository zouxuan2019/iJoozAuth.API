using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace iJoozAuth.API.Service
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IConfiguration _configuration;

        public CustomResourceOwnerPasswordValidator(IConfiguration cofiguration)
        {
            _configuration = cofiguration;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var url = _configuration["UserManagement:Host"] + "/Verify";
            var userInfo = new {userName = context.UserName, password = context.Password};
            
            using (var client = new HttpClient())
            {
                var stringContent =
                    new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");
                var httpResponseMessage = client.PostAsync(url, stringContent).GetAwaiter().GetResult();
                if (httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult().Equals("true"))
                {
                    context.Result =
                        new GrantValidationResult(context.UserName, OidcConstants.AuthenticationMethods.Password);
                }
            }

            return Task.FromResult(0);
        }
    }
}