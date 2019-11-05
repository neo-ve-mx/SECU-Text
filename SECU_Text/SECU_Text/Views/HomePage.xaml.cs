using SECU_Text.Models;
using SECU_Text.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SECU_Text.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        public HomePage()
        {
            InitializeComponent();
            
            MasterPage.ListViewMasterPage.ItemSelected += ListViewMasterPage_ItemSelected;
            DetailPage.ListViewDetailPage.ItemSelected += ListViewDetailPage_ItemSelected;
        }

        private void ListViewMasterPage_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageMasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListViewMasterPage.SelectedItem = null;
        }

        private void ListViewDetailPage_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageDetailEntryItem;

            if (item == null)
                return;

            T_Entry t_Entry = new T_Entry();
            t_Entry.Id = item.Id; 
            t_Entry.Icon = item.Icon; 
            t_Entry.IconTitle = item.IconTitle; 
            t_Entry.Title = item.Title;
            t_Entry.Content = item.Content;

            //var page = (Page)Activator.CreateInstance(item.TargetType);
            //page.Title = item.Title;

            MainViewModel.GetInstance().ViewItem = new ViewItemViewModel(t_Entry);
            Application.Current.MainPage.Navigation.PushAsync(new ViewItemPage());

            //Detail = new NavigationPage(page);
            IsPresented = false;

            DetailPage.ListViewDetailPage.SelectedItem = null;
        }
    }
}