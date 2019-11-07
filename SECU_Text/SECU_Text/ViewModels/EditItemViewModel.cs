using GalaSoft.MvvmLight.Command;
using SECU_Text.Models;
using SECU_Text.Services;
using SECU_Text.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static SECU_Text.ViewModels.AddItemViewModel;

namespace SECU_Text.ViewModels
{
    public class EditItemViewModel : BaseViewModel
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

        public T_Entry EntryData { get; set; }
        #endregion

        #region Constructors
        public EditItemViewModel(T_Entry data)
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            db = platform.GetConnection();

            #region List Types Items
            ItemType itemType;
            List<ItemType> itemTypes = new List<ItemType>();
            itemType = new ItemType();
            itemType.iconType = "pass_item";
            itemType.nameType = "Claves de Acceso";
            itemTypes.Add(itemType);
            itemType = new ItemType();
            itemType.iconType = "bank_item";
            itemType.nameType = "Información Bancaria";
            itemTypes.Add(itemType);
            itemType = new ItemType();
            itemType.iconType = "phone_item";
            itemType.nameType = "Número Telefónico";
            itemTypes.Add(itemType);
            itemType = new ItemType();
            itemType.iconType = "card_item";
            itemType.nameType = "Tarjetas de Pago";
            itemTypes.Add(itemType);
            TypesList = itemTypes;
            #endregion

            if (data.Id != 0)
            {
                EntryData = new T_Entry();
                EntryData = data;
                this.TypeItemIndex = data.IconIndex;
                this.TitleItem = data.Title;
                this.ContentItem = Base64Decode(data.Content);
            }
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
                    "ERROR",
                    "Seleccione un tipo de entrada.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.TitleItem))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Ingrese un título para la entrada.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.ContentItem))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Ingrese el contenido para la entrada.",
                    "Aceptar");
                return;
            }

            bool result = await Application.Current.MainPage.DisplayAlert("SECU-Text", "Desea actualizar la entrada?", "Si", "No");
            if (result)
            {
                try
                {
                    var registerData = new T_Entry { Id = EntryData.Id,  Icon = TypeItem.iconType, IconTitle = TypeItem.nameType, IconIndex = TypeItemIndex, Title = TitleItem, Content = Base64Encode(ContentItem) };
                    var resultDB = db.Update(registerData);
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
                        await Application.Current.MainPage.DisplayAlert("ERROR", "No se pudo actualizar la entrada.", "Aceptar");
                        return;
                    }
                }
                catch (SQLiteException sqlex)
                {
                    await Application.Current.MainPage.DisplayAlert("ERROR", "Ocurrió un error.", "Aceptar");
                    return;
                }
            }
        }
        #endregion

        #region Helpers
        private static string Base64Encode(string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodeBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }
        #endregion
    }
}
