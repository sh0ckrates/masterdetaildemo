using Prism.Navigation;
using Xamarin.Forms;

namespace EcareMob.Views
{
    public partial class RootPage : MasterDetailPage , IMasterDetailPageOptions
    {
        public RootPage()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;

    }
}