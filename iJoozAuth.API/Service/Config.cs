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
                    ClientId = SupportedClients.FvMembershipClientId.ToString(),
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess = true,
                    ClientSecrets = new List<Secret> {new Secret("FvMembershipClientSecret".Sha256())},
                    AllowedScopes =
                    {
                        SupportedApis.FvMembership.ToString()
                    
                    }
                },
                new Client
                {
                    ClientId = SupportedClients.FvMembershipThirdPartyClientId.ToString(),
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientSecrets = new List<Secret> {new Secret("FvMembershipThirdPartyClientSecret".Sha256())},
                    AllowedScopes =
                    {
                        SupportedApis.FvMembership.ToString()
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
                new ApiResource(SupportedApis.FvMembership.ToString(), "FvMembership API")
            };
        }

        public enum SupportedApis
        {
            FvMembership
        }

        public enum SupportedClients
        {
            FvMembershipClientId,
            FvMembershipThirdPartyClientId
        }
    }
}