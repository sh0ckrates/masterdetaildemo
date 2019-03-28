using System.Threading.Tasks;
using EcareMob.Clients;

namespace EcareMob.Services
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string username, string password, IDataClient dataClient);

        Task<bool> LogoutAsync();

        string GetTokenAsync();

        bool IsAuthenticated { get; }
        string GetError();

        /// <summary>
        /// Used only for demo, to quickly bypass actual authentication
        /// </summary>
        /// <returns>Task</returns>
        void BypassAuthentication();

        Task RefreshTokenAsync(bool force=false);
        Task PersistClientInfo();
        bool HasError();
    }
}

