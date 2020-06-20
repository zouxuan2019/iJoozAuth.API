using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using iJoozAuth.API.Models;
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
            var url = _configuration["UserManagement:Host"] + "/login";
            var userInfo = new {username = context.UserName, password = context.Password};

//            using (var client = new HttpClient())
//            {
//                var stringContent =
//                    new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");
//                var httpResponseMessage = client.PostAsync(url, stringContent).GetAwaiter().GetResult();
//                var result = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
//                var loginResult = JsonConvert.DeserializeObject<LoginResult>(result);
//                if (loginResult.status.Equals("1"))
//                {
//                    context.Result =
//                        new GrantValidationResult(context.UserName, OidcConstants.AuthenticationMethods.Password);
//                }
//            }
            context.Result =
                new GrantValidationResult(context.UserName, OidcConstants.AuthenticationMethods.Password);

            return Task.FromResult(0);
        }
    }
}