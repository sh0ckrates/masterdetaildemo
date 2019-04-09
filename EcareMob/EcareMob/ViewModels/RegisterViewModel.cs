using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Helpers;
using EcareMob.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace EcareMob.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;


        private string _vatNumber;
        public string VatNumber
        {
            get { return _vatNumber; }
            set { SetProperty(ref _vatNumber, value); }
        }

        private string _contractNo;
        public string ContractNo
        {
            get { return _contractNo; }
            set { SetProperty(ref _contractNo, value); }
        }


        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }


        private string _repeatPassword;

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { SetProperty(ref _repeatPassword, value); }
        }

        private string _pageLabel;

        public string PageLabel
        {
            get { return _pageLabel; }
            set { SetProperty(ref _pageLabel, value); }
        }



        private string _registerMessage;
        public string RegisterMessage
        {
            get => _registerMessage;
            set
            {
                _registerMessage = value;
                RaisePropertyChanged();
            }
        }


        public DelegateCommand RegisterCommand { get; set; }

        public RegisterViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;
            Title = "Εγγραφή";
            PageLabel = "Συμπληρώστε τα παρακάτω στοιχεία για την εγγραφή σας";



            //RegisterCommand = new DelegateCommand(async () => await Register());

        }




        private async Task Register()
        {

            await Pass(async () =>
            {
                RegisterMessage = "";
                if (await Authenticate())
                {
                    RegisterMessage = "Success";
                    
                    //Settings.UserName = Username;


                    var res = await _dataClient.GetUserInfo(Settings.UserId);

                    if (res != null)
                    {
                        Settings.FullName = res.FullName;
                        Settings.CharismaCode = res.CharismaCode;
                    }


                    await NavigationService.NavigateAsync("/RootPage/NavigationPage/MainPage");

                }
                else
                {
                    RegisterMessage = "Login_Failed";
                }

            });

        }





        async Task<bool> Authenticate()
        {
            bool success = false;
            try
            {
                var encryptedPassword = Encryption.Encrypt(Password, Settings.EncKey);

                //success = await _authenticator.AuthenticateAsync(Username, encryptedPassword, _dataClient);
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

    }
}
