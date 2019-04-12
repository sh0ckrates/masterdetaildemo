using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients;
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
        public DelegateCommand<UserProfile> LoadCommand { get; set; }


        private IUserDialogs _dialog;
        private readonly IDataClient _dataClient;




        public ContractsViewModel(INavigationService navigationService, IAuthenticationService authenticator, IUserDialogs dialog, IDataClient dataClient)
            : base(navigationService, dialog)
        {
            _dialog = dialog;
            _authenticator = authenticator;
            _dataClient = dataClient;

            Title = "Τα Συμβόλαιά μου";
            //LoadCommand = new DelegateCommand<UserProfile>(async (x) => await LoadProfile());
            //Task.Run(async () => { await LoadProfile(); }).Wait();

        }




    }
}
