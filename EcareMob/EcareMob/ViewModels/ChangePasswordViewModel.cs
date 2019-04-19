using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Helpers;
using EcareMob.Models;
using EcareMob.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace EcareMob.ViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
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


        private string _previousPassword;

        public string PreviousPassword
        {
            get { return _previousPassword; }
            set { SetProperty(ref _previousPassword, value); }
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



        private string _changePassMessage;
        public string ChangePassMessage
        {
            get => _changePassMessage;
            set
            {
                _changePassMessage = value;
                RaisePropertyChanged();
            }
        }


        public DelegateCommand ChangePasswordCommand { get; set; }

        public ChangePasswordViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;
            Title = "Αλλαγή Κωδικού Πρόσβασης";
            PageLabel = "Συμπληρώστε τα παρακάτω στοιχεία για να αλλάξετε τον κωδικό πρόσβασής σας.";

            ChangePasswordCommand = new DelegateCommand(async () => await ChangePassword())
                .ObservesProperty(() => RepeatPassword)
                .ObservesProperty(() => PreviousPassword)
                .ObservesProperty(() => Password); 
        }


        private async Task ChangePassword()
        {
            if (await ValidateForm())
            {
                await Pass(async () =>
                {
                    ChangePassMessage = "";

                    var newPassModel = new ChangePasswordModel()
                    {
                        NewPassword = Encryption.Encrypt(Password, Settings.EncKey),
                        UserId = Settings.UserId
                    };

                    var res = await _dataClient.ChangePassword(newPassModel);

                    if (res != null && !res.Success)
                    {
                        ChangePassMessage = res.Message;
                    }

                    if (res != null && res.Success)
                    {
                        await Dialog.AlertAsync(res.Message, "Επιτυχής Αλλαγή Κωδικού", "Σύνδεση");
                        await NavigationService.NavigateAsync("/Login");

                        //var goToLogin = await Dialog.ConfirmAsync(res.Message, "Επιτυχής Αλλαγή Κωδικού", "Σύνδεση", "Επιστροφή");
                        //if (goToLogin)
                        //{
                        //    await NavigationService.NavigateAsync("/Login");
                        //}
                    }
                });
            }
        }

        private async Task<bool> ValidateForm()
        {
            if (Password != null &&  PreviousPassword != null && RepeatPassword != null)
            {
                return await ValidatePassword(Password, RepeatPassword);
            }

            ChangePassMessage = "Παρακαλώ συμπληρώστε όλα τα πεδία";
            return false;
        }




        private async Task<bool> ValidatePassword(string password, string repeatedPassword)
        {
            if (password != repeatedPassword)
            {
                ChangePassMessage = "Oι κωδικοί δεν ταιριάζουν";
                return false;
            }

            string passwordRegex = "^(?=.*\\d+)(?=.*[a-zA-Z])[0-9a-zA-Z!@#$%]{8,}$";
            var matchPass = Regex.Match(password, passwordRegex, RegexOptions.IgnoreCase);
            var matchConfirmPass = Regex.Match(repeatedPassword, passwordRegex, RegexOptions.IgnoreCase);
            if (matchPass.Success && matchConfirmPass.Success)
            {
                return true;
            }
            else
            {
                ChangePassMessage = "O κωδικός πρέπει να αποτελέιται απο 8 χαρακτήρες, με τουλάχιστον 1 αριθμό";
                return false;
            }

        }






    }
}
