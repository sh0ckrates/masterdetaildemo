using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Helpers;
using EcareMob.Services;
using Prism.Services;
using Thesis.Invetory.Shared.Services;

namespace EcareMob.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;

        private string _userName;
        public string Username
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }


        private string _welcomeMessage;

        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set { SetProperty(ref _welcomeMessage, value); }
        }



        private string _loginMessage;
        public string LoginMessage
        {
            get => _loginMessage;
            set
            {
                _loginMessage = value;
                RaisePropertyChanged();
            }
        }


        public DelegateCommand LoginCommand { get; set; }




        public LoginViewModel(INavigationService navigationService, IAuthenticationService authedicator, IUserDialogs dialog)
            : base(navigationService , dialog)
        {

            Title = "Bmw Customer Web Portal";
            WelcomeMessage = "Καλώς ήρθατε στην προσωπική σας σελίδα στο portal πελατών της BMW Financial Services!";

            LoginCommand = new DelegateCommand(async () => await Login());
        }




        private async Task Login()
        {
            await Pass(async () =>
            {
                LoginMessage = "";
                if  (await Authenticate())
                {
                    LoginMessage = "Success";
                    //await ViewModel.LoadInitialData();
                    Settings.UserName = Username;
                    //App.IsLoggedIn = true;
                    //await NavigationService.NavigateAsync("/RootPage/NavigationPage/MainPage");
                    await NavigationService.NavigateAsync("NavigationPage/MainPage");

                }
                else
                {
                    LoginMessage = "Login_Failed";
                }

            });

        }

        



        async Task<bool> Authenticate()
        {
            bool success = false;
            try
            {
                success = await _authenticator.AuthenticateAsync(Username, Password);
                if (success)
                {
                    //RaisePropertyChanged(ProfileImage);

                    await _dialogService.DisplayAlertAsync("Alert", "Login Successfull", "OK");

                }

            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Alert", ex.Message, "OK");
            }
            return success;
        }
    }
}
