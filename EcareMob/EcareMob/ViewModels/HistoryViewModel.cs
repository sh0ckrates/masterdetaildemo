using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace EcareMob.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;
        public DelegateCommand LoadCommand { get; set; }


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;


        private ObservableRangeCollection<History> _historyRows;
        public ObservableRangeCollection<History> HistoryRows
        {
            get => _historyRows;
            set
            {
                _historyRows = value;
                RaisePropertyChanged();
            }
        }



        public HistoryViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;

            Title = "Ιστορικό";




            HistoryRows = new ObservableRangeCollection<History>();
            LoadCommand = new DelegateCommand(async () => await Load());

            //LoadCommand = new DelegateCommand<UserProfile>(async (x) => await LoadProfile());
            //Task.Run(async () => { await LoadProfile(); }).Wait();

        }


        private async Task Load()
        {
            await Pass(async () =>
            {
                NoItemsFound = false;
                HistoryRows.Clear();
                var list = await _dataClient.GetHistory(Settings.Vat);
                var x = list;
                HistoryRows.AddRange(list);
                if (list == null)
                    NoItemsFound = true;
            });
        }


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Load();
            // base.OnNavigatedTo(parameters);
        }


    }
}
