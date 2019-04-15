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
    public abstract class ViewModelBase : BindableBase, INavigationAware, IConfirmNavigation, IDestructible
    {

        const string RootUriPrependText = "/";
        protected const string LoadingMessageConstant = "Loading items...";
        DelegateCommand<string> _navigateAbsoluteCommand;
        DelegateCommand<string> _navigateCommand;
        DelegateCommand<string> _navigateModalCommand;
        DelegateCommand<string> _navigateNonModalCommand;

        /// <summary>
        /// Gets the navigate absolute command.
        /// </summary>
        /// <value>The navigate absolute command.</value>
        public DelegateCommand<string> NavigateAbsoluteCommand => _navigateAbsoluteCommand ?? (_navigateAbsoluteCommand = new DelegateCommand<string>(NavigateAbsoluteCommandExecute, CanNavigateAbsoluteCommandExecute));

        /// <summary>
        /// Gets the navigate command.
        /// </summary>
        /// <value>The navigate command.</value>
        public DelegateCommand<string> NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(NavigateCommandExecute, CanNavigateCommandExecute));

        /// <summary>
        /// Gets the navigate modal command.
        /// </summary>
        /// <value>The navigate modal command.</value>
        public DelegateCommand<string> NavigateModalCommand => _navigateModalCommand ?? (_navigateModalCommand = new DelegateCommand<string>(NavigateModalCommandExecute, CanNavigateModalCommandExecute));

        /// <summary>
        /// Gets the navigate non modal command.
        /// </summary>
        /// <value>The navigate non modal command.</value>
        public DelegateCommand<string> NavigateNonModalCommand => _navigateNonModalCommand ?? (_navigateNonModalCommand = new DelegateCommand<string>(NavigateNonModalCommandExecute, CanNavigateNonModalCommandExecute));

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        /// <value>The navigation service.</value>
        protected INavigationService NavigationService { get; }

        protected IUserDialogs Dialog { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialog">Dialog Service</param>
        /// <exception cref="System.ArgumentNullException">navigationService</exception>
        protected ViewModelBase(INavigationService navigationService, IUserDialogs dialog)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException(nameof(navigationService));
            }
            this.NavigationService = navigationService;
            Dialog = dialog;
            NoItemsFoundMessage = "Δεν βρέθηκαν αποτελέσματα";
            LoadingMessage = LoadingMessageConstant;
        }



        /// <summary>
        /// Determines whether this instance can execute the NavigateAbsoluteCommand.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns><c>true</c> if this instance can execute NavigateAbsoluteCommand; otherwise, <c>false</c>.</returns>
        protected virtual bool CanNavigateAbsoluteCommandExecute(string uri)
        {
            return !String.IsNullOrEmpty(uri);
        }

        /// <summary>
        /// Determines whether this instance can execute the NavigateAbsoluteCommand.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns><c>true</c> if this instance can execute NavigateAbsoluteCommand; otherwise, <c>false</c>.</returns>
        protected virtual bool CanNavigateCommandExecute(string uri)
        {
            return !String.IsNullOrEmpty(uri);
        }

        /// <summary>
        /// Determines whether this instance can execute the NavigateModalCommand.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns><c>true</c> if this instance can execute NavigateModalCommand; otherwise, <c>false</c>.</returns>
        protected virtual bool CanNavigateModalCommandExecute(string uri)
        {
            return !String.IsNullOrEmpty(uri);
        }

        /// <summary>
        /// Determines whether this instance can execute the NavigateNonModalCommand.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <returns><c>true</c> if this instance can execute NavigateNonModalCommand; otherwise, <c>false</c>.</returns>
        protected virtual bool CanNavigateNonModalCommandExecute(string uri)
        {
            return !String.IsNullOrEmpty(uri);
        }

        /// <summary>
        /// <p>Invoked by Prism Navigation when the instance is removed from the navigation stack.</p>
        /// <p>Deriving class can override and perform any required clean up.</p>
        /// </summary>
        public virtual void Destroy()
        {
        }

        /// <summary>
        /// Navigates to the uri after creating a new navigation root. (Effectively replacing the Application MainPage.)
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        protected virtual async void NavigateAbsoluteCommandExecute(string uri)
        {
            if (!CanNavigateAbsoluteCommandExecute(uri))
            {
                return;
            }
            if (!uri.StartsWith(RootUriPrependText))
            {
                uri = string.Concat(RootUriPrependText, uri);
            }
            await this.NavigationService.NavigateAsync(uri, null, false);
        }

        /// <summary>
        /// Navigates to the uri.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        protected virtual async void NavigateCommandExecute(string uri)
        {
            if (!CanNavigateCommandExecute(uri))
            {
                return;
            }
            await this.NavigationService.NavigateAsync(uri);
        }

        /// <summary>
        /// Navigates to the uri using a Modal navigation.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        protected virtual async void NavigateModalCommandExecute(string uri)
        {
            if (!CanNavigateModalCommandExecute(uri))
            {
                return;
            }
            await this.NavigationService.NavigateAsync(uri, useModalNavigation: true);
        }

        /// <summary>
        /// Navigates to the uri using Non-Modal navigation.
        /// </summary>
        /// <param name="uri">The uri text.</param>
        /// <returns>Task.</returns>
        protected virtual async void NavigateNonModalCommandExecute(string uri)
        {
            if (!CanNavigateNonModalCommandExecute(uri))
            {
                return;
            }
            await this.NavigationService.NavigateAsync(uri, useModalNavigation: false);
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


        ////--Inerfaces Implemented--//
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        public virtual bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
