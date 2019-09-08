using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using iJoozAuth.API.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Http;

namespace iJoozAuth.API.Service
{
    public class JwtTokenGenerator
    {
        private IdentityServerTools _identityServerTools;
        private IHttpContextAccessor _httpcontextaccessor;

        public JwtTokenGenerator(IdentityServerTools identityServerTools,
            IHttpContextAccessor httpcontextaccessor)
        {
            _identityServerTools = identityServerTools;
            _httpcontextaccessor = httpcontextaccessor;
        }

        public string GenerateJwtToken(UserInfo userInfo)
        {
            var issuer = GetHostUri();
            var token = _identityServerTools.IssueJwtAsync(
                (int) userInfo.expires_at,
                issuer,
                GetClaims(userInfo, issuer)
            ).GetAwaiter().GetResult();
            return token;
        }

        private static IEnumerable<Claim> GetClaims(UserInfo userInfo, string issuer)
        {
            var claims = new List<Claim>();

            var claimsPart1 = new[]
            {
                new Claim("aud", userInfo.application),
                new Claim("client_id", Config.SupportedClients.FvMembershipClientId.ToString()),
                new Claim("sub", userInfo.name),
                new Claim("auth_time", ((DateTimeOffset) DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new Claim("idp", issuer),
                new Claim("amr", "client_credentials"),
                new Claim("email", userInfo.email),
            };
            claims.AddRange(claimsPart1);
            claims.AddRange(GetScopeClaims(userInfo));
            return claims;
        }

        private static IEnumerable<Claim> GetScopeClaims(UserInfo userInfo)
        {
            return userInfo.scopes != null
                ? userInfo.scopes.Select(scope => new Claim("scope", scope))
                : new List<Claim>();
        }

        private string GetHostUri()
        {
            var request = _httpcontextaccessor.HttpContext.Request;
            var absoluteUri = string.Concat(
                request.Scheme,
                "://",
                request.Host.ToUriComponent());
            return absoluteUri;
        }
    }
}