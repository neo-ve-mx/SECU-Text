using GalaSoft.MvvmLight.Command;
using SECU_Text.Models;
using SECU_Text.Services;
using SECU_Text.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class HomePageDetailViewModel : BaseViewModel
    {
        #region Atributes
        private SQLiteConnection db;
        private ObservableCollection<HomePageDetailEntryItem> entryitems;
        #endregion

        #region Properties
        public ObservableCollection<HomePageDetailEntryItem> EntryItems
        {
            get { return entryitems; }
            set { SetValue(ref entryitems, value); }
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
        #endregion

        #region Constructors
        public HomePageDetailViewModel()
        {
            try
            {
                var platform = DependencyService.Get<ISQLitePlatform>();
                db = platform.GetConnection();
                IEnumerable<T_Entry> getEntryItems = SELECT_WHERE(db);
                EntryItems = new ObservableCollection<HomePageDetailEntryItem>();
                HomePageDetailEntryItem homePageDetailEntryItem;
                foreach (T_Entry entry in getEntryItems)
                {
                    homePageDetailEntryItem = new HomePageDetailEntryItem();
                    homePageDetailEntryItem.Id = entry.Id;
                    homePageDetailEntryItem.Icon = entry.Icon;
                    homePageDetailEntryItem.IconTitle = entry.IconTitle;
                    homePageDetailEntryItem.IconIndex = entry.IconIndex;
                    homePageDetailEntryItem.Title = entry.Title;
                    homePageDetailEntryItem.Content = entry.Content;
                    homePageDetailEntryItem.TargetType = typeof(ViewItemPage);
                    EntryItems.Add(homePageDetailEntryItem);
                }
            }
            catch (SQLiteException sqlex)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", "Ocurrió un error.", "Aceptar");
                return;
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
            MainViewModel.GetInstance().AddItem = new AddItemViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddItemPage());
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
                    IEnumerable<T_Entry> getEntryItems = SELECT_WHERE(db);
                    EntryItems = new ObservableCollection<HomePageDetailEntryItem>();
                    HomePageDetailEntryItem homePageDetailEntryItem;
                    foreach (T_Entry entry in getEntryItems)
                    {
                        homePageDetailEntryItem = new HomePageDetailEntryItem();
                        homePageDetailEntryItem.Id = entry.Id;
                        homePageDetailEntryItem.Icon = entry.Icon;
                        homePageDetailEntryItem.IconTitle = entry.IconTitle;
                        homePageDetailEntryItem.IconIndex = entry.IconIndex;
                        homePageDetailEntryItem.Title = entry.Title;
                        homePageDetailEntryItem.Content = entry.Content;
                        homePageDetailEntryItem.TargetType = typeof(ViewItemPage);
                        EntryItems.Add(homePageDetailEntryItem);
                    }

                    IsRefreshing = false;
                });
            }
        }
        #endregion

        #region Helpers
        private static IEnumerable<T_Entry> SELECT_WHERE(SQLiteConnection db)
        {
            return db.Query<T_Entry>("SELECT * FROM T_Entry order by Icon");
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodeBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }
        #endregion
    }
}
