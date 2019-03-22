using System;
using System.Linq;
using System.Threading.Tasks;
using EcareMob.Clients;
using EcareMob.Helpers;
using EcareMob.Models;
using EcareMob.Services;
using IdentityModel.Client;

namespace EcareMob.Services
{
    public class AuthService : IAuthenticationService
    {

        private TokenResponse _authenticationResult;

        private IDataClient _dataClient;

        public AuthService()
        {

        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var authResult = await RequestToken(username, password);
            if (!authResult.AuthenticationError)
            {
                Settings.ExpireTokenDate = DateTime.UtcNow.AddSeconds(5000);
                Settings.AccessToken = authResult.Token;
                //Settings.RefreshToken = authResult.RefreshToken;
                //await PersistClientInfo();
            }

            return !authResult.AuthenticationError;
        }

        private async Task<User> RequestToken(string username, string password)
        {
            var authPostModel = new User()
            {
                Username = username,
                Password = password
            };
            var res = await _dataClient.GetAuthentication(authPostModel);

            return res;
        }

        public async Task<bool> LogoutAsync()
        {

            //await GetConfigAsync();

            //await Task.Factory.StartNew(async () =>
            //    { 
            //        var success = await _Authenticator.DeAuthenticate(_TenantAuthority);
            //        if (!success)
            //        {
            //            throw new Exception("Failed to DeAuthenticate!");
            //        }
            //        _AuthenticationResult = null;
            //    });
            return true;
        }















        //Not used yet 

        public async Task RefreshTokenAsync(bool force = false)
        {
            var expire = DateTime.UtcNow.AddSeconds(60);
            if (expire < Settings.ExpireTokenDate && !force)
                return;
            var client = GetClient();
            _authenticationResult = await client.RequestRefreshTokenAsync(Settings.RefreshToken);
            if (!_authenticationResult.IsError)
            {
                Settings.ExpireTokenDate = DateTime.Now.AddSeconds(_authenticationResult.ExpiresIn);
                Settings.AccessToken = _authenticationResult.AccessToken;
                Settings.RefreshToken = _authenticationResult.RefreshToken;
            }

        }

        public string GetTokenAsync()
        {
            return _authenticationResult.AccessToken;
        }

        public string GetError()
        {
            return _authenticationResult.Error;
        }

        public bool HasError()
        {
            return _authenticationResult.IsError;
        }

        public bool IsAuthenticated
        {
            get
            {
                if (_AuthenticationBypassed)
                    return true;
                return !string.IsNullOrEmpty(_authenticationResult?.AccessToken);
            }
        }

        private bool _AuthenticationBypassed;

        /// <summary>
        /// Used only for demo, to quickly bypass actual authentication
        /// </summary>
        /// <returns>Task</returns>
        public void BypassAuthentication()
        {
            _AuthenticationBypassed = true;
        }

        private TokenClient GetClient()
        {
            return new TokenClient(
               Settings.ServerUrl + "identity/connect/token",
               "native",
               "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
        }

        private UserInfoClient GetUserInfoClient()
        {
            return new UserInfoClient(Settings.ServerUrl + "identity/connect/userinfo");
        }

        public async Task PersistClientInfo()
        {
            var client = GetUserInfoClient();
            var userinfo = await client.GetAsync(Settings.AccessToken);
            if (userinfo.ErrorType == ResponseErrorType.None)
            {
                //Settings.UserId = Convert.ToInt32(userinfo.Claims.FirstOrDefault(x=>x.Type == "uid").Value);
                Settings.FullName = userinfo.Claims.FirstOrDefault(x => x.Type == "given_name").Value;
            }
        }
    }
}

