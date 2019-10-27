using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using iJoozAuth.API.Models;
using iJoozAuth.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace iJoozAuth.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeTokenController : ControllerBase
    {
        private readonly FacebookService _facebookService;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public ExchangeTokenController(FacebookService facebookService, JwtTokenGenerator jwtTokenGenerator)
        {
            _facebookService = facebookService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpGet("basic")]
        public string Test()
        {
            var credentials = string.Format("{0}:{1}", Config.SupportedClients.FvMembershipClientId.ToString(),
                "FvMembershipClientSecret");
            var headerValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            return headerValue;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
        }

        [HttpPost("jwt")]
        public async Task<IActionResult> GetJwtToken([FromBody] ExchangeTokenRequest exchangeTokenRequest)
        {
            return GenerateToken(exchangeTokenRequest, exchangeTokenRequest.Token);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshJwtToken([FromBody] ExchangeTokenRequest exchangeTokenRequest)
        {
            string refreshToken = _facebookService.RefreshFacebookToken(exchangeTokenRequest.Token);
            if (refreshToken == null)
            {
                return Unauthorized("Invalid token1");
            }

            exchangeTokenRequest.Token = refreshToken;
            return GenerateToken(exchangeTokenRequest, refreshToken);
        }

        private IActionResult GenerateToken(ExchangeTokenRequest exchangeTokenRequest, string refreshToken)
        {
            var validTokenInfo = GetValidTokenInfo(exchangeTokenRequest);
            if (validTokenInfo == null)
            {
                return Unauthorized("Invalid token");
            }

            long unixTime = ((DateTimeOffset) DateTime.UtcNow).ToUnixTimeSeconds();
            int lifeTime = (int) (validTokenInfo.expires_at - unixTime);
            return Ok(new ExchangeTokenResponse
            {
                expires_in = lifeTime,
                access_token = _jwtTokenGenerator.GenerateJwtToken(validTokenInfo, lifeTime),
                token_type = "Bearer",
                refresh_token = refreshToken,
                source = exchangeTokenRequest.Source
            });
        }


        private UserInfo GetValidTokenInfo(ExchangeTokenRequest exchangeTokenRequest)
        {
            return "facebook".Equals(exchangeTokenRequest.Source.ToLower())
                ? _facebookService.GetUserInfoByToken(exchangeTokenRequest.Token)
                : null;
        }
    }
}