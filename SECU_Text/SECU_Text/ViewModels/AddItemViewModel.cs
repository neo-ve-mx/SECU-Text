using GalaSoft.MvvmLight.Command;
using SECU_Text.Views;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
        #region Atributes
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
            #endregion

            TypesList = itemTypes;
            TypeItemIndex = -1;
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
                bool result = await Application.Current.MainPage.DisplayAlert("ALERTA", "Desea cancelar los cambios?", "Si", "No");
                if (result)
                {
                    MainViewModel.GetInstance().AddItem = new AddItemViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                }
            }
            else
            {
                MainViewModel.GetInstance().AddItem = new AddItemViewModel();
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
    }
}
