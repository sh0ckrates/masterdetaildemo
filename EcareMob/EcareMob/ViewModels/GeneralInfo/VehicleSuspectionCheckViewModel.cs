using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace EcareMob.ViewModels.GeneralInfo
{

        public class VehicleSuspectionCheckViewModel : ViewModelBase
        {
            private readonly IAuthenticationService _authenticator;
            private readonly IPageDialogService _dialogService;


            private IUserDialogs _dialog;
            private readonly IDataClient _dataClient;

            public DelegateCommand MiniIconTappedCommand { get; set; }
            public DelegateCommand BmwΙconTappedCommand { get; set; }


        public VehicleSuspectionCheckViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
                : base(navigationService, dialog)
            {
                _dialog = dialog;
                _authenticator = authenticator;
                _dataClient = dataClient;

                Title = "Έλεγχος Κατάστασης Οχήματος";

                MiniIconTappedCommand = new DelegateCommand(async () => await DownloadPdfFile("mini"));
                BmwΙconTappedCommand = new DelegateCommand(async () => await DownloadPdfFile("bmw"));

            //LoadCommand = new DelegateCommand<UserProfile>(async (x) => await LoadProfile());
            //Task.Run(async () => { await LoadProfile(); }).Wait();

        }


        //private async Task Load()
        //{
        //    await Pass(async () =>
        //    {


        //    });
        //}




        //public override async void OnNavigatingTo(INavigationParameters parameters)
        //{

        //    //if (parameters.ContainsKey(NavigationParams.PartId))
        //    //{
        //    //    _partId = parameters.GetValue<string>(NavigationParams.PartId);
        //    //}

        //    await Load();

        //}


        private async Task DownloadPdfFile(string brand)
        {
            string url = null;
                
                if (brand!= null)
                {
                    url = brand == "bmw" ? "http://files.ethesis.eu/ecare/files/Έλεγχος_Κατάστασης_BMW.pdf" : "http://files.ethesis.eu/ecare/files/Έλεγχος_Κατάστασης_MINI.pdf";
                }

                if (string.IsNullOrWhiteSpace(url))
                    return;

                Device.OpenUri(new Uri(url));
            }



    }

}
