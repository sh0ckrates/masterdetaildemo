using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Helpers;
using EcareMob.Models;
using EcareMob.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace EcareMob.ViewModels.GeneralInfo
{
    public class PaymentMethodsViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;

        private string _methodsHtml;

        public string MethodsHtml
        {
            get => _methodsHtml;
            set
            {
                _methodsHtml = value;
                RaisePropertyChanged();
            }
        }

        public PaymentMethodsViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;

            Title = "Τρόποι Πληρωμής";



            //LoadCommand = new DelegateCommand<UserProfile>(async (x) => await LoadProfile());
            //Task.Run(async () => { await LoadProfile(); }).Wait();

        }


        private async Task LoadMethods()
        {
            await Pass(async () =>
            {
                string url = "http://files.ethesis.eu/ecare/generalinformation/PaymentMethods.txt";

                var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };

                MethodsHtml = client.DownloadString(url);
            });
        }




        public override async void OnNavigatedTo(INavigationParameters parameters)
        {

            //if (parameters.ContainsKey(NavigationParams.PartId))
            //{
            //    _partId = parameters.GetValue<string>(NavigationParams.PartId);
            //}

            await LoadMethods();

        }





    }
}
