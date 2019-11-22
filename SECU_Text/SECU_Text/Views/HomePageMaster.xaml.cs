using SECU_Text.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SECU_Text.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        #region Atributes
        public ListView ListViewMasterPage;
        #endregion

        public HomePageMaster()
        {
            InitializeComponent();
            ListViewMasterPage = MenuItemsListView;
        }
    }
}