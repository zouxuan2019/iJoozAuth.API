using System.Collections.Generic;
using System.Linq;
using IdentityServer4;
using IdentityServer4.Models;

namespace iJoozAuth.API.Service
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ijoozClientId",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess = true,
                    ClientSecrets = new List<Secret> {new Secret("ijoozClientIdSecret".Sha256())},
                    AllowedScopes =
                    {
                        SupportedApis.ijooz.ToString()
                    
                    }
                },
                new Client
                {
                    ClientId = "thirdParty",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientSecrets = new List<Secret> {new Secret("thirdPartySecret".Sha256())},
                    AllowedScopes =
                    {
                        SupportedApis.ijooz.ToString()
                    }
                }
            };
        }


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource(SupportedApis.ijooz.ToString(), "IJooz API"),
            };
        }

        public enum SupportedApis
        {
            ijooz
            
        }
    }
}