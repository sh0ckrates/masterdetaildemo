using System;
using Xamarin.Forms;

namespace EcareMob.Views
{
    public partial class Contact : ContentPage
    {
        public Contact()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
