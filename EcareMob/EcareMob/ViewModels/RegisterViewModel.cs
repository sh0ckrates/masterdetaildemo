using System;
using System.Net.Mail;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Helpers;
using EcareMob.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EcareMob.Models;

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




            RegisterCommand = new DelegateCommand(async () => await Register());
        }


        private async Task Register()
        {
            if (await ValidateForm())
            {
                await Pass(async () =>
                {
                    RegisterMessage = "";

                    var registerModel = new RegisterModel
                    {
                        Password = Encryption.Encrypt(Password , Settings.EncKey) ,
                        Email = Email,
                        VatNumber = VatNumber,
                        ContractNumber = ContractNo
                    };

                    var res = await _dataClient.RegisterNewUser(registerModel);

                    if (res != null && !res.Success)
                    {
                        RegisterMessage = res.Message;
                    }

                    if (res != null && res.Success)
                    {
                        var goToLogin = await Dialog.ConfirmAsync(res.Message, "Επιτυχής εγγραφή", "Σύνδεση", "Επιστροφή");
                        if (goToLogin)
                        {
                            await NavigationService.NavigateAsync("/Login");
                        }
                    }


                    //await NavigationService.NavigateAsync("/Login");

                });
            }
        }

        private async Task<bool> ValidateForm()
        {
            if (Password != null && Email != null && VatNumber != null && ContractNo != null)
            {
                if (!(await IsValidEmail(Email)))
                {
                    RegisterMessage = "Παρακαλώ εισάγετε μία έγκυρη διεύθυνση email";
                    return false;
                }

                if (await ValidatePassword(Password, RepeatPassword))
                {
                    return true;
                }
            }
            else
            {
                RegisterMessage = "Παρακαλώ συμπληρώστε όλα τα πεδία";
            }
            return false;
        }







        private async Task<bool> ValidatePassword(string password, string repeatedPassword)
        {
            if (password != repeatedPassword)
            {
                RegisterMessage = "Oι κωδικοί δεν ταιριάζουν";
                return false;
            }

            string passwordRegex = "^(?=.*\\d+)(?=.*[a-zA-Z])[0-9a-zA-Z!@#$%]{8,}$";
            var matchPass = Regex.Match(password, passwordRegex, RegexOptions.IgnoreCase);
            var matchConfirmPass = Regex.Match(repeatedPassword, passwordRegex, RegexOptions.IgnoreCase);
            if (matchPass.Success)
            {
                return true;
            }
            else
            {
                RegisterMessage = "O κωδικός πρέπει να αποτελέιται απο 8 χαρακτήρες, με τουλάχιστον 1 αριθμό";
                return false;
            }

        }


        public async Task<bool> IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }



    }
}
