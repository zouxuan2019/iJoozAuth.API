using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using iJoozAuth.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace iJoozAuth.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private HttpContext _currentContext;

        public TokenController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _currentContext = httpContextAccessor.HttpContext;
            _configuration = configuration;
        }

        private string GetBaseUrl()
        {
            var request = _currentContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var baseUrl = GetBaseUrl();
            var tokenUrl = $"{baseUrl}/connect/token";
            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "password");
            dict.Add("client_id", _configuration["FvMembership:ClientId"]);
            dict.Add("client_secret", _configuration["FvMembership:ClientSecret"]);
            dict.Add("scope", _configuration["FvMembership:Scope"]);
            dict.Add("username", loginRequest.username);
            dict.Add("password", loginRequest.password);
            using (var client = new HttpClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Post, tokenUrl) {Content = new FormUrlEncodedContent(dict)};
                var res = await client.SendAsync(req);
                return Ok(res.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Login([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var baseUrl = GetBaseUrl();
            var tokenUrl = $"{baseUrl}/connect/token";
            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "refresh_token");
            dict.Add("client_id", _configuration["FvMembership:ClientId"]);
            dict.Add("client_secret", _configuration["FvMembership:ClientSecret"]);
            dict.Add("refresh_token", refreshTokenRequest.refresh_token);

            using (var client = new HttpClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Post, tokenUrl) {Content = new FormUrlEncodedContent(dict)};
                var res = await client.SendAsync(req);
                return Ok(res.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
        }
    }
}