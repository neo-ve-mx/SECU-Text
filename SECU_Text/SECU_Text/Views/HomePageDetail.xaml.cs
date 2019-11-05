using SECU_Text.Models;
using SECU_Text.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SECU_Text.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageDetail : ContentPage
    {
        #region Atributes
        public ListView ListViewDetailPage; 
        #endregion

        public HomePageDetail()
        {
            InitializeComponent();
            ListViewDetailPage = EntryItemsListView;
        }

        private void EntryItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        //private void LV_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    MainViewModel.GetInstance().ViewItem = new ViewItemViewModel((T_Entry)this.LV.SelectedItem);
        //    Application.Current.MainPage.Navigation.PushAsync(new ViewItemPage());
        //}
    }
}