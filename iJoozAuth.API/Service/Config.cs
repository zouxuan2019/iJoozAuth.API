using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace iJoozAuth.API.Service
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
// client credentials, list of clients
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

// Client secrets
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                },
                new Client
                {
                    ClientId = "ijoozClientId",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess = true,
                    ClientSecrets = new List<Secret> {new Secret("ijoozClientIdSecret".Sha256())},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId
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
                new IdentityResources.Email()
            };
        }
    }
}