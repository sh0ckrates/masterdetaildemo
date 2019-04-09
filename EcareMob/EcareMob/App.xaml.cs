using System;
using Acr.UserDialogs;
using EcareMob.Clients;
using EcareMob.Clients.Base;
using EcareMob.Services;
using Prism;
using Prism.Ioc;
using EcareMob.ViewModels;
using EcareMob.Views;
using Plugin.Iconize;
using Prism.DryIoc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EcareMob
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        static Application _app;
        public static Application CurrentApp => _app;

        public App(): this(null)
        {



        }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

            Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule())
                .With(new Plugin.Iconize.Fonts.IoniconsModule())
                .With(new Plugin.Iconize.Fonts.SimpleLineIconsModule());
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/Login");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRequestProvider, RequestProvider>();
            containerRegistry.Register<IDataClient, DataClient>();
            containerRegistry.Register<IAuthenticationService, AuthService>();
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<RootPage, RootPageViewModel>();

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Login, LoginViewModel>();
            containerRegistry.RegisterForNavigation<Contact, ContactViewModel>();
            containerRegistry.RegisterForNavigation<Profile, ProfileViewModel>();



        }



    public static void SetLogoutSettings()
        {
            Helpers.Settings.RefreshToken = "";
            Helpers.Settings.AccessToken = "";
            Helpers.Settings.ExpireTokenDate = DateTime.Now.AddYears(-10);
        }





    }
}
