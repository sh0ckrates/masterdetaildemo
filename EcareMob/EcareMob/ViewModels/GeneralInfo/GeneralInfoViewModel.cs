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

namespace EcareMob.ViewModels.GeneralInfo
{
    public class GeneralInfoViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;
        public DelegateCommand LoadCommand { get; set; }


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;






        public GeneralInfoViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;

            Title = "Γενικές Πληροφορίες";




            LoadCommand = new DelegateCommand(async () => await Load());

            //LoadCommand = new DelegateCommand<UserProfile>(async (x) => await LoadProfile());
            //Task.Run(async () => { await LoadProfile(); }).Wait();

        }


        private async Task Load()
        {
            await Pass(async () =>
            {

            });
        }


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Load();
            // base.OnNavigatedTo(parameters);
        }


    }
}
