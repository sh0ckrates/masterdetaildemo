using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Models;
using EcareMob.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace EcareMob.ViewModels.GeneralInfo
{
    public class EndOfTermProcessViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;

        public DelegateCommand LoadCommand { get; set; }

        private string _endOfTermHtml;
        public string EndOfTermHtml
        {
            get => _endOfTermHtml;
            set
            {
                _endOfTermHtml = value;
                RaisePropertyChanged();
            }
        }

        public EndOfTermProcessViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;

            Title = "ΔΙΑΔΙΚΑΣΙΑ ΛΗΞΗΣ ΣΥΜΒΟΛΑΙΟΥ";



            LoadCommand = new DelegateCommand (async () => await LoadEndOfTermHtml());
            //Task.Run(async () => { await LoadEndOfTermHtml(); }).Wait();

        }



        private async Task LoadEndOfTermHtml()
        {
            await Pass(async () =>
            {
                string url = "http://files.ethesis.eu/ecare/generalinformation/EndOfTermMob.txt";


                var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };

                EndOfTermHtml = client.DownloadString(url);

                //await _dialog.AlertAsync(EndOfTermHtml, "Alert", "OK");

            });
        }
         



        public override async void OnNavigatingTo(INavigationParameters parameters)
        {

            //if (parameters.ContainsKey(NavigationParams.PartId))
            //{
            //    _partId = parameters.GetValue<string>(NavigationParams.PartId);
            //}

            await LoadEndOfTermHtml();

        }
    }
}
