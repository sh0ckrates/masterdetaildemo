using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Services;
using Plugin.Messaging;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace EcareMob.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;


        private string _siteAddress;
        private string _phoneNumber;
        private string _faxNumber;
        private string _emailAddress;

        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                RaisePropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged();
            }
        }


        public string FaxNumber
        {
            get => _faxNumber;
            set
            {
                _faxNumber = value;
                RaisePropertyChanged();
            }
        }

        public string SiteAddress
        {
            get => _siteAddress;
            set
            {
                _siteAddress = value;
                RaisePropertyChanged();
            }
        }




        private string _contactTitle;

        public string ContactTitle
        {
            get { return _contactTitle; }
            set { SetProperty(ref _contactTitle, value); }
        }

        private string _contactText;

        public string ContactText
        {
            get { return _contactText; }
            set { SetProperty(ref _contactText, value); }
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



        public DelegateCommand WebsiteIconTappedCommand { get; set; }
        public DelegateCommand PhoneIconTappedCommand { get; set; }
        public DelegateCommand EmailIconTappedCommand { get; set; }
        public DelegateCommand MapIconTappedCommand { get; set; }
        public DelegateCommand HelpFileIconTappedCommand { get; set; }




        public ContactViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;
            Title = "Επικοινωνία";
            ContactTitle = "BMW Financial Services";
            ContactText = "Επικοινωνήστε με το Τμήμα Εξυπηρέτησης Πελατών.Κάθε μέρα στη διάθεσή σας για οποιαδήποτε ερώτηση!";
            FaxNumber = "210-9118505";


            PhoneNumber = "210-9118500";
            SiteAddress = "https://www.mybmwbank.gr";
            EmailAddress = "infosf@bmw.gr";
            //WebsiteIconTappedCommand = new DelegateCommand(async () => await GoToWebsite());
            //PhoneIconTappedCommand = new DelegateCommand(async () => await MakePhoneCall());
            //EmailIconTappedCommand = new DelegateCommand(async () => await SendMail());
        }



        private async Task GoToWebsite()
        {
            if (String.IsNullOrWhiteSpace(SiteAddress))
                return;
            Device.OpenUri(new Uri(SiteAddress));
        }

        private async Task MakePhoneCall()
        {
            if (String.IsNullOrWhiteSpace(PhoneNumber))
                return;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(PhoneNumber.Replace(" ", "").Replace("-",""));
        }

        private async Task SendMail()
        {
            if (String.IsNullOrWhiteSpace(PhoneNumber))
                return;
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                emailMessenger.SendEmail(EmailAddress);
            }
        }
    }
}
