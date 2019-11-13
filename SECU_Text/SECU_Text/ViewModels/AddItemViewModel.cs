using GalaSoft.MvvmLight.Command;
using SECU_Text.Helpers;
using SECU_Text.Models;
using SECU_Text.Services;
using SECU_Text.Views;
using SQLite;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
        #region Atributes
        private SQLiteConnection db;
        private ItemType typeitem;
        private int typeitemindex;
        private string titleitem;
        private string contentitem;
        #endregion

        #region Properties
        public int TypeItemIndex 
        {
            get { return typeitemindex; }
            set { SetValue(ref typeitemindex, value); }
        }
        public ItemType TypeItem
        {
            get { return typeitem; }
            set { SetValue(ref typeitem, value); }
        }

        public string TitleItem
        {
            get { return titleitem; }
            set { SetValue(ref titleitem, value); }
        }

        public string ContentItem
        {
            get { return contentitem; }
            set { SetValue(ref contentitem, value); }
        }

        public List<ItemType> TypesList { get; set; }
        #endregion

        #region Constructores
        public AddItemViewModel()
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            db = platform.GetConnection();

            #region List Types Items
            ItemType itemType;
            List<ItemType> itemTypes = new List<ItemType>();
            itemType = new ItemType();
            itemType.iconType = "pass_item";
            itemType.nameType = Languages.ItemTypeLiteral1;
            itemTypes.Add(itemType);
            itemType = new ItemType();
            itemType.iconType = "bank_item";
            itemType.nameType = Languages.ItemTypeLiteral2;
            itemTypes.Add(itemType);
            itemType = new ItemType();
            itemType.iconType = "phone_item";
            itemType.nameType = Languages.ItemTypeLiteral3;
            itemTypes.Add(itemType);
            itemType = new ItemType();
            itemType.iconType = "card_item";
            itemType.nameType = Languages.ItemTypeLiteral4;
            itemTypes.Add(itemType);
            itemType = new ItemType();
            itemType.iconType = "text_item";
            itemType.nameType = Languages.ItemTypeLiteral5;
            itemTypes.Add(itemType);
            TypesList = itemTypes;
            TypeItemIndex = -1;
            #endregion
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (this.TypeItem == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1,
                    Languages.AddItemLiteral1,
                    Languages.ExceptionLiteral3);
                return;
            }

            if (string.IsNullOrEmpty(this.TitleItem))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1,
                    Languages.AddItemLiteral2,
                    Languages.ExceptionLiteral3);
                return;
            }

            if (string.IsNullOrEmpty(this.ContentItem))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1,
                    Languages.AddItemLiteral3,
                    Languages.ExceptionLiteral3);
                return;
            }

            bool result = await Application.Current.MainPage.DisplayAlert(
                Languages.AppLiteral1, 
                Languages.AddItemLiteral4, 
                Languages.ViewItemLiteral3, 
                Languages.ViewItemLiteral4);
            if (result)
            {
                try
                {
                    var registerData = new T_Entry { Icon = TypeItem.iconType, IconTitle = TypeItem.nameType, IconIndex = TypeItemIndex, Title = TitleItem, Content = Base64Encode(ContentItem) };
                    var resultDB = db.Insert(registerData);
                    if (resultDB == 1)
                    {
                        TypeItemIndex = -1;
                        TitleItem = string.Empty;
                        ContentItem = string.Empty;

                        MainViewModel.GetInstance().HomePageDetail = new HomePageDetailViewModel();
                        await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.ExceptionLiteral1, 
                            Languages.AddItemLiteral5, 
                            Languages.ExceptionLiteral3);
                        return;
                    }
                }
                catch (SQLiteException sqlex)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.ExceptionLiteral1, 
                        Languages.ExceptionLiteral2, 
                        Languages.ExceptionLiteral3);
                    return;
                }
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(Close);
            }
        }

        private async void Close()
        {
            if (this.TypeItem != null || !string.IsNullOrEmpty(this.TitleItem) || !string.IsNullOrEmpty(this.ContentItem))
            {
                bool result = await Application.Current.MainPage.DisplayAlert(
                    Languages.AppLiteral1, 
                    Languages.AddItemLiteral6, 
                    Languages.ViewItemLiteral3, 
                    Languages.ViewItemLiteral4);
                if (result)
                {
                    MainViewModel.GetInstance().HomePage = new HomePageViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                }
            }
            else
            {
                MainViewModel.GetInstance().HomePage = new HomePageViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
        }
        #endregion

        #region Objects
        public class ItemType
        {
            public string iconType { get; set; }
            public string nameType { get; set; }
        }
        #endregion

        #region Helpers
        private static string Base64Encode(string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        #endregion
    }
}
