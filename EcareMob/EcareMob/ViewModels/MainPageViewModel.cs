using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;

namespace EcareMob.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
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


        public MainPageViewModel(INavigationService navigationService, IUserDialogs dialog)
            : base(navigationService ,dialog)
        {
            Title = "Bmw Customer Web Portal";
            WelcomeMessage = "";//Καλώς ήρθατε στην προσωπική σας σελίδα στο portal πελατών της BMW Financial Services!";


        }
    }
}
