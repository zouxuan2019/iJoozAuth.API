using System;
using System.Net.Http;
using iJoozAuth.API.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace iJoozAuth.API.Service
{
    public class FacebookService
    {
        private readonly IConfiguration _configuration;

        public FacebookService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private FacebookDebugTokenResponse GetTokenInfo(string token)
        {
            var host = _configuration["Facebook:Host"];
            var url = $"{host}/debug_token?input_token={token}&access_token={token}";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var result = httpClient.GetStringAsync(url).GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<FacebookDebugTokenResponse>(result);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public UserInfo GetUserInfoByToken(string token)
        {
            var tokenInfo = GetTokenInfo(token);
            if (!IsTokenValid(tokenInfo))
            {
                return null;
            }

            var userInfo = GetUserInfo(tokenInfo.data.user_id, token);
            if (userInfo == null)
            {
                return null;
            }

            userInfo.expires_at = tokenInfo.data.expires_at;
            userInfo.scopes = tokenInfo.data.scopes;

            return userInfo;
        }

        private bool IsTokenValid(FacebookDebugTokenResponse response)
        {
            if (response == null)
                return false;
            var data = response.data;
            return data.is_valid
                   && data.app_id.Equals(_configuration["Facebook:AppId"])
                   && data.application.Equals(_configuration["Facebook:Application"])
                   && data.scopes.Contains(_configuration["Facebook:scopes"]);
        }

        private UserInfo GetUserInfo(string userId, string token)
        {
            var host = _configuration["Facebook:Host"];
            var url = $"{host}/{userId}?fields=name,email&access_token={token}";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var result = httpClient.GetStringAsync(url).GetAwaiter().GetResult();
                    var userInfo = JsonConvert.DeserializeObject<UserInfo>(result);
                    return userInfo;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}