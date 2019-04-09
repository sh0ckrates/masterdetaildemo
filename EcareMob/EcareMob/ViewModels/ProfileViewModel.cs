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
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;
        public DelegateCommand<UserProfile> LoadCommand { get; set; }


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;

        public UserProfile UserProfile { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ProfileViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;

            Title = "Το Προφίλ μου";
            LoadCommand = new DelegateCommand<UserProfile>(async (x) => await LoadProfile());
            Task.Run(async () => { await LoadProfile(); }).Wait();

        }



        private async Task LoadProfile()
        {
            await Pass(async () =>
            {
                UserProfile = await _dataClient.GetUserProfile(Settings.UserId);
            });
        }


        public override async void OnNavigatedTo(NavigationParameters parameters)
        {

            //if (parameters.ContainsKey(NavigationParams.PartId))
            //{
            //    _partId = parameters.GetValue<string>(NavigationParams.PartId);
            //}

            await LoadProfile();

            base.OnNavigatedTo(parameters);
        }

    }
}
