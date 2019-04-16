using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcareMob.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcareMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage
    {
        public History()
        {
            InitializeComponent();
            //TestOrdersRepository model = new TestOrdersRepository();
            //BindingContext = model;
        }
    }
}