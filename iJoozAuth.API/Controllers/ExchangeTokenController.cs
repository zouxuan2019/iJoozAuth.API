using System;
using System.Threading.Tasks;
using iJoozAuth.API.Models;
using iJoozAuth.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace iJoozAuth.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
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
            var validTokenInfo = GetValidTokenInfo(exchangeTokenRequest);
            if (validTokenInfo == null)
            {
                return Unauthorized("Invalid token");
            }

            long unixTime = ((DateTimeOffset) DateTime.UtcNow).ToUnixTimeSeconds();
            return Ok(new ExchangeTokenResponse
            {
                access_token = _jwtTokenGenerator.GenerateJwtToken(validTokenInfo),
                expires_in = validTokenInfo.expires_at - unixTime,
                token_type = "Bearer"
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