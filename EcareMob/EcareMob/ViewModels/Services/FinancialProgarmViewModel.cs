using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Services;
using Prism.Navigation;
using Prism.Services;

namespace EcareMob.ViewModels.Services
{

    public class FinancialProgarmViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticator;
        private readonly IPageDialogService _dialogService;


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;

        private string _financialHtml;

        public string FinancialHtml
        {
            get => _financialHtml;
            set
            {
                _financialHtml = value;
                RaisePropertyChanged();
            }
        }

        public FinancialProgarmViewModel(INavigationService navigationService, IAuthenticationService authenticator,
            IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;

            Title = "ΧΡΗΜΑΤΟΔΟΤΙΚΑ ΠΡΟΓΡΑΜΜΑΤΑ";



            //LoadCommand = new DelegateCommand<UserProfile>(async (x) => await LoadPage());
            //Task.Run(async () => { await LoadProfile(); }).Wait();

        }


        private async Task LoadPage()
        {
            await Pass(async () =>
            {

                var res = await _dataClient.GetResource("FinancialProducts.Content1", "el-GR");

                if (res!= null && res.Value != null)
                {
                    FinancialHtml = res.Value;
                }

            });
        }




        public override async void OnNavigatedTo(INavigationParameters parameters)
        {

            //if (parameters.ContainsKey(NavigationParams.PartId))
            //{
            //    _partId = parameters.GetValue<string>(NavigationParams.PartId);
            //}

            await LoadPage();

        }
    }
}

