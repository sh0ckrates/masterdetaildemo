using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using EcareMob.Helpers;
using EcareMob.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace EcareMob.ViewModels
{
    public class RootPageViewModel : ViewModelBase
    {
        public DelegateCommand<MyMenuItem> NavigateToCommand { get; set; }

        private MyMenuItem _selectedMenuItem;
        public MyMenuItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                if (value == _selectedMenuItem)
                    return;
                _selectedMenuItem = value;
                //_selectedMenuItem.FColor = Color.LightSlateGray;//(Color)Application.Current.Resources["Primary"];
                //UpdateMenuItems(_selectedMenuItem);
                RaisePropertyChanged();
            }
        }

        //private void UpdateMenuItems(MyMenuItem selectedMenuItem)
        //{
        //    var theRest = MenuItems.Where(c => c.MenuType != selectedMenuItem.MenuType);
        //    theRest.ToList().ForEach(c => c.FColor = Color.Gray);
        //}

        public string FullName => Settings.FullName;


        public RootPageViewModel(INavigationService navigationService, IUserDialogs dialog) : base(navigationService, dialog)
        {
            NavigateToCommand = new DelegateCommand<MyMenuItem>(Navigate);
            MenuItems = new ObservableCollection<MyMenuItem>
            {
                new MyMenuItem()
                {
                    Title = "Τα συμβόλαιά μου",
                    Uri = "/RootPage/NavigationPage/Contracts",
                    Icon = "chapters_49_navigation_default.png",
                    MenuType = MenuType.Contracts,
                    FColor = Color.Gray
                },
                new MyMenuItem()
                {
                    Title = "Ιστορικό",
                    Uri = "/RootPage/NavigationPage/History",
                    Icon = "outlook_49_communication_default.png",
                    MenuType = MenuType.Profile,
                    FColor = Color.Gray
                },
                new MyMenuItem()
                {
                    Title = "Το προφίλ μου",
                    Uri = "/RootPage/NavigationPage/Profile",
                    Icon = "profile_49_navigation_default.png",
                    MenuType = MenuType.Profile,
                    FColor = Color.Gray
                },
                //new MyMenuItem()
                //{
                //    Title = "Αλλαγή κωδικού",
                //    Uri = "NavigationPage/Settings",
                //    Icon = "ic_settings_black_24dp.png",
                //    MenuType = MenuType.Settings,
                //    FColor = Color.Gray
                //},
                 new MyMenuItem()
                {
                    Title = "Επικοινωνία",
                    Uri = "/RootPage/NavigationPage/Contact",
                    Icon = "phone_49_communication_default.png",
                    MenuType = MenuType.Contact,
                    FColor = Color.Gray
                },
                 new MyMenuItem()
                {
                    Title = "'Εξοδος",
                    Uri = "/Login",
                    Icon = "logout_49_actions_default.png",
                    MenuType = MenuType.Logout,
                    FColor = Color.Gray
                }

            };

            //SelectedMenuItem = MenuItems.FirstOrDefault(x => x.MenuType == MenuType.Contact);
        }

        private void Navigate(MyMenuItem item)
        {
            switch (item.MenuType)
            {
                case MenuType.Logout:
                    App.SetLogoutSettings();
                    break;
                default:
                    break;
                    ;
            }
           // item.FColor = Color.Bisque;
            NavigationService.NavigateAsync(item.Uri);
        }

        ObservableCollection<MyMenuItem> _menuItems;
        public ObservableCollection<MyMenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                RaisePropertyChanged();
            }
        }
    }
}
