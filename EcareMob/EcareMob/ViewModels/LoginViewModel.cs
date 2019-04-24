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
using EcareMob.Clients;
using EcareMob.Models;
using EcareMob.Views;
using Xamanimation;
using Xamarin.Forms;

namespace EcareMob.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;


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


        private string _welcomeMessage1;

        public string WelcomeMessage1
        {
            get { return _welcomeMessage1; }
            set { SetProperty(ref _welcomeMessage1, value); }
        }


        private string _welcomeMessage2;

        public string WelcomeMessage2
        {
            get { return _welcomeMessage2; }
            set { SetProperty(ref _welcomeMessage2, value); }
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

        
        public DelegateCommand GotoRegisterCommand { get; set; }

        public DelegateCommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;
            Title = "Bmw Customer Web Portal";
            WelcomeMessage1 = "Καλώς ήρθατε στον προσωπικό σας χώρο στο portal πελατών της BMW Financial Services!";
            WelcomeMessage2 = "Με τη νέα αυτή υπηρεσία έχετε άμεση πρόσβαση στα στοιχεία του συμβολαίου σας εύκολα, γρήγορα και με ασφάλεια, 24 ώρες το 24ωρο.";



            LoginCommand = new DelegateCommand(async () => await Login() , LoginCommandCanExecute)
                .ObservesProperty(() => Username)
                .ObservesProperty(() => Password); 
            GotoRegisterCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/Register"));
        }




        private async Task Login()
        {

            await Pass(async () =>
            {
                IsBusy = true;
                LoginMessage = "";
                if (await Authenticate())
                {
                    LoginMessage = "Success";
                    //await ViewModel.LoadInitialData();
                    Settings.UserName = Username;


                    var res = await _dataClient.GetUserInfo(Settings.UserId);

                    if (res != null)
                    {
                        Settings.FullName = res.FullName;
                        Settings.CharismaCode = res.CharismaCode;
                        Settings.Vat = res.Vat;
                    }

                    //App.IsLoggedIn = true;
                    await NavigationService.NavigateAsync("/RootPage/NavigationPage/History");
                    //await NavigationService.NavigateAsync("/RootPage/NavigationPage/Contracts", null, false, true);

                    //await NavigationService.NavigateAsync(new System.Uri("RootPage/NavigationPage/Contracts", System.UriKind.Relative), useModalNavigation: false);

                    //await this.NavigationService.NavigateAsync("/RootPage/NavigationPage/Contracts",
                    //    useModalNavigation: false);


                }
                else
                {
                    LoginMessage = "Login Failed";
                }
                IsBusy = false;
            });

        }





        async Task<bool> Authenticate()
        {
            bool success = false;
            try
            {
                var encryptedPassword = Encryption.Encrypt(Password, Settings.EncKey);

                success = await _authenticator.AuthenticateAsync(Username, encryptedPassword, _dataClient);
                if (success)
                {
                    //RaisePropertyChanged(ProfileImage);

                    //await _dialog.AlertAsync("Login Successfull", "Alert", "OK");
                    
                }

            }
            catch (Exception ex)
            {
                await _dialog.AlertAsync(ex.Message, "Alert", "OK");
            }
            return success;
        }



        private bool LoginCommandCanExecute() =>
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !IsBusy;


       // public override async void OnNavigatingTo(INavigationParameters parameters)
        //{
            //await InitDb();
            //await Login();
        //}


    }
}
