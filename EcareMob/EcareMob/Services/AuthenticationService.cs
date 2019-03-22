//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using EcareMob.Helpers;
//using EcareMob.Services;
//using IdentityModel.Client;

//namespace Thesis.Invetory.Shared.Services
//{
//    public class AuthenticationService : IAuthenticationService
//    {
        
//        private TokenResponse _authenticationResult;

//        public AuthenticationService()
//        {
            
//        }

//        public async Task<bool> AuthenticateAsync(string username, string password)
//        {
//            _authenticationResult = await RequestToken(username, password);
//            if (!_authenticationResult.IsError)
//            {
//                Settings.ExpireTokenDate = DateTime.UtcNow.AddSeconds(_authenticationResult.ExpiresIn);
//                Settings.AccessToken = _authenticationResult.AccessToken;
//                Settings.RefreshToken = _authenticationResult.RefreshToken;
//                await PersistClientInfo();
//            }
//            return !_authenticationResult.IsError;
//        }

//        private async Task<TokenResponse> RequestToken(string username, string password)
//        {
//            var client = GetClient();
//             var res = await client.RequestResourceOwnerPasswordAsync(username, password, "cgsoftapi openid roles profile email offline_access");
//            return res;
//        }
//        public async Task<bool> LogoutAsync()
//        {
            
//            //await GetConfigAsync();

//            //await Task.Factory.StartNew(async () =>
//            //    { 
//            //        var success = await _Authenticator.DeAuthenticate(_TenantAuthority);
//            //        if (!success)
//            //        {
//            //            throw new Exception("Failed to DeAuthenticate!");
//            //        }
//            //        _AuthenticationResult = null;
//            //    });
//            return true;
//        }

//        public async Task RefreshTokenAsync(bool force = false)
//        {
//            var expire = DateTime.UtcNow.AddSeconds(60);
//            if (expire < Settings.ExpireTokenDate && !force)
//                return;
//            var client = GetClient();
//            _authenticationResult = await client.RequestRefreshTokenAsync(Settings.RefreshToken);
//            if (!_authenticationResult.IsError)
//            {
//                Settings.ExpireTokenDate = DateTime.Now.AddSeconds(_authenticationResult.ExpiresIn);
//                Settings.AccessToken = _authenticationResult.AccessToken;
//                Settings.RefreshToken = _authenticationResult.RefreshToken;
//            }
            
//        }

//        public string GetTokenAsync()
//        {
//            return _authenticationResult.AccessToken;
//        }

//        public string GetError()
//        {
//            return _authenticationResult.Error;
//        }

//        public bool HasError()
//        {
//            return _authenticationResult.IsError;
//        }

//        public bool IsAuthenticated
//        {
//            get
//            {
//                if (_AuthenticationBypassed)
//                    return true;
//                return !string.IsNullOrEmpty(_authenticationResult?.AccessToken);
//            }
//        }

//        private bool _AuthenticationBypassed;

//        /// <summary>
//        /// Used only for demo, to quickly bypass actual authentication
//        /// </summary>
//        /// <returns>Task</returns>
//        public void BypassAuthentication()
//        {
//            _AuthenticationBypassed = true;
//        }

//        private TokenClient GetClient()
//        {
//            return new TokenClient(
//               Settings.ServerUrl + "identity/connect/token",
//               "native",
//               "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
//        }

//        private UserInfoClient GetUserInfoClient()
//        {
//            return new UserInfoClient(Settings.ServerUrl + "identity/connect/userinfo");
//        }

//        public async Task PersistClientInfo()
//        {
//            var client = GetUserInfoClient();
//            var userinfo = await client.GetAsync(Settings.AccessToken);
//            if (userinfo.ErrorType == ResponseErrorType.None)
//            {
//                //Settings.UserId = Convert.ToInt32(userinfo.Claims.FirstOrDefault(x=>x.Type == "uid").Value);
//                Settings.FullName = userinfo.Claims.FirstOrDefault(x => x.Type == "given_name").Value;
//            }
//        }
//    }
//}

