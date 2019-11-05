using GalaSoft.MvvmLight.Command;
using SECU_Text.Models;
using SECU_Text.Services;
using SECU_Text.Views;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Atributes
        private SQLiteConnection db;
        private List<T_Entry> itemlist;
        private T_Entry listviewitemselected;
        #endregion

        #region Properties
        public List<T_Entry> ItemsList
        {
            get { return itemlist; }
            set { SetValue(ref itemlist, value); }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public T_Entry ListViewItemSelected
        {
            get { return listviewitemselected; }
            set { SetValue(ref listviewitemselected, value); }
        }
        #endregion

        #region Constructors
        public HomeViewModel()
        {
            try
            {
                var platform = DependencyService.Get<ISQLitePlatform>();
                db = platform.GetConnection();

                #region List Items
                IEnumerable<T_Entry> getItems = SELECT_WHERE(db);
                ItemsList = getItems.ToList<T_Entry>();
                #endregion
            }
            catch (SQLiteException sqlex)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", "Ocurrió un error.", "Aceptar");
            }
        }
        #endregion

        #region Commands
        public ICommand NewItemCommand
        {
            get
            {
                return new RelayCommand(NewItem);
            }
        }

        private async void NewItem()
        {
            //MainViewModel.GetInstance().Home = new HomeViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new AddItemPage());
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    var platform = DependencyService.Get<ISQLitePlatform>();
                    db = platform.GetConnection();

                    #region List Items
                    IEnumerable<T_Entry> getItems = SELECT_WHERE(db);
                    ItemsList = getItems.ToList<T_Entry>();
                    #endregion

                    IsRefreshing = false;
                });
            }
        }

        public ICommand SelectedCommand
        {
            get
            {
                return new RelayCommand(Selected);
            }
        }

        private async void Selected()
        {
            string aaa = ListViewItemSelected.Title;
        }
        #endregion

        #region Helpers
        private static IEnumerable<T_Entry> SELECT_WHERE(SQLiteConnection db)
        {
            return db.Query<T_Entry>("SELECT * FROM T_Item order by Icon");
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodeBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }
        #endregion
    }
}
