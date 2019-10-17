using System;
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
        {            var validTokenInfo = GetValidTokenInfo(exchangeTokenRequest);
            if (validTokenInfo == null)
            {
                return Unauthorized("Invalid token");
            }

            long unixTime = ((DateTimeOffset) DateTime.UtcNow).ToUnixTimeSeconds();
            int lifeTime = (int)(validTokenInfo.expires_at - unixTime);
            return Ok(new ExchangeTokenResponse
            {
                expires_in = lifeTime,
                access_token = _jwtTokenGenerator.GenerateJwtToken(validTokenInfo,lifeTime),
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