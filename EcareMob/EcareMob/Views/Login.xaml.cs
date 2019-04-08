using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamanimation;
using Xamarin.Forms;

namespace EcareMob.Views
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoginLogo.FadeTo(0, 20);

            await Task.Delay(1000);

            await Task.WhenAll(
                  LoginLogo.FadeTo(1, 2000)
                  //LoginLogo.FadeTo(1, 1000)
                  //FadeBox.Animate(new FadeInAnimation());
            );
        }
    }

}