using GalaSoft.MvvmLight.Command;
using SECU_Text.Models;
using SECU_Text.Services;
using SECU_Text.Views;
using SQLite;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class ViewItemViewModel : BaseViewModel
    {
        #region Atributes
        private SQLiteConnection db;
        private string icon;
        private string icontitle;
        private string title;
        private string content;
        private bool isrunning;
        #endregion

        #region Properties
        public string Icon
        {
            get { return icon; }
            set { SetValue(ref icon, value); }
        }

        public string IconTitle
        {
            get { return icontitle; }
            set { SetValue(ref icontitle, value); }
        }

        public string Title
        {
            get { return title; }
            set { SetValue(ref title, value); }
        }

        public string Content
        {
            get { return content; }
            set { SetValue(ref content, value); }
        }

        public bool IsRunning
        {
            get { return isrunning; }
            set { SetValue(ref isrunning, value); }
        }

        public T_Entry EntryData { get; set; }
        #endregion

        #region Constructors
        public ViewItemViewModel(T_Entry data)
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            db = platform.GetConnection();

            if (data.Id != 0)
            {
                this.EntryData = data;
                this.Icon = data.Icon;
                this.IconTitle = data.IconTitle;
                this.Title = data.Title;
                this.Content = Base64Decode(data.Content);
            }
        }
        #endregion

        #region Commands
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }

        private async void Edit()
        {
            MainViewModel.GetInstance().EditItem = new EditItemViewModel(EntryData);
            await Application.Current.MainPage.Navigation.PushAsync(new EditItemPage());
        }

        public ICommand CopyCommand
        {
            get
            {
                return new RelayCommand(Copy);
            }
        }

        private async void Copy()
        {
            await Clipboard.SetTextAsync(Content);
            if (Clipboard.HasText)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "SECU-Text",
                    "Datos de la entrada copiados al portapapeles.",
                    "Aceptar");
                return;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        private async void Delete()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("SECU-Text", "Desea eliminar la entrada actual?", "Si", "No");
            if (result)
            {
                try
                {
                    var resultDB = db.Delete(EntryData);
                    if (resultDB == 1)
                    {
                        MainViewModel.GetInstance().HomePageDetail = new HomePageDetailViewModel();
                        await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("ERROR", "No se pudo eliminar la entrada actual.", "Aceptar");
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
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodeBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }
        #endregion
    }
}
