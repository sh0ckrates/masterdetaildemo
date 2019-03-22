using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EcareMob.Clients.Base;

namespace EcareMob.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected IUserDialogs Dialog { get; }

        protected const string LoadingMessageConstant = "Loading...";


        protected ViewModelBase(INavigationService navigationService, IUserDialogs dialog)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException(nameof(navigationService));
            }
            this.NavigationService = navigationService;
            Dialog = dialog;
            NoItemsFoundMessage = "No items Found";
            LoadingMessage = LoadingMessageConstant;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }






        private bool _noitemsfound;
        private string _noitemsfoundmessage;
        private string _loadingmessage;
        private string _title;
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public bool NoItemsFound
        {
            get => _noitemsfound;
            set
            {
                _noitemsfound = value;
                RaisePropertyChanged();
            }
        }

        public string NoItemsFoundMessage
        {
            get => _noitemsfoundmessage;
            set
            {
                _noitemsfoundmessage = value;
                RaisePropertyChanged();
            }
        }

        public string LoadingMessage
        {
            get => _loadingmessage;
            set
            {
                _loadingmessage = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }




        protected async Task Pass(Func<Task> action, bool enablebusy = true)
        {
            if (enablebusy)
                IsBusy = true;
            try
            {
                await action();
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await Dialog.AlertAsync("Comunication Error", "Error", "Ok");
            }
            catch (Exception ex) when (ex is ServiceManagedErrorException)
            {
                await Dialog.AlertAsync(ex.Message, "Comunication Error", "Ok");
            }
            catch (Exception ex) when (ex is ServiceAuthenticationException)
            {
                await Dialog.AlertAsync(ex.Message, "Authentication Error", "Ok");
            }
            catch (Exception ex) when (ex is ValidateException)
            {
                await Dialog.AlertAsync(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data in: {ex}");
            }
            if (enablebusy)
                IsBusy = false;
        }
    }
}
