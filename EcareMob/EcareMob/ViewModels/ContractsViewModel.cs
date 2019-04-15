using System;
using System.Collections.Generic;
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
    public class ContractsViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;
        public DelegateCommand LoadCommand { get; set; }


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;


        private ObservableRangeCollection<Contract> _myContracts;
        public ObservableRangeCollection<Contract> MyContracts
        {
            get => _myContracts;
            set
            {
                _myContracts = value;
                RaisePropertyChanged();
            }
        }

        public ContractsViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;

            Title = "Τα Συμβόλαιά μου";

            MyContracts = new ObservableRangeCollection<Contract>();
            LoadCommand = new DelegateCommand(async () => await Load());

            //LoadCommand = new DelegateCommand<UserProfile>(async (x) => await LoadProfile());
            //Task.Run(async () => { await LoadProfile(); }).Wait();

        }

        private async Task Load()
        {
            await Pass(async () =>
            {
                NoItemsFound = false;
                MyContracts.Clear();
                var list = await _dataClient.GetMyContracts(Settings.CharismaCode,Settings.Vat);
                MyContracts.AddRange(list);
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
