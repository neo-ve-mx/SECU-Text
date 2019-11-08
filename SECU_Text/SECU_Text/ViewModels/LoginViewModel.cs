using GalaSoft.MvvmLight.Command;
using SECU_Text.Views;
using SECU_Text.Services;
using SQLite;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using SECU_Text.Models;

namespace SECU_Text.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Atributes
        private SQLiteConnection db;
        private string password;
        private bool isrunning;
        private bool isenabled;
        private string _password;
        #endregion

        #region Properties
        public string Password 
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }
        public bool IsRunning 
        {
            get { return isrunning; }
            set { SetValue(ref isrunning, value); }
        }
        public bool IsEnabled 
        {
            get { return isenabled; }
            set { SetValue(ref isenabled, value); }
        }
        #endregion

        #region Constructores
        public LoginViewModel()
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            db = platform.GetConnection();

            this.IsEnabled = true;

            //this.Password = "n30v3mx";
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", "Ingrese su contraseña.", "Aceptar");
                return;
            }

            //this.IsRunning = true;
            this.IsEnabled = false;
            try
            {
                IEnumerable<T_Appuser> getAppUser = SELECT_WHERE(db, "ApplicationUser");
                T_Appuser t_Appuser = getAppUser.ToList<T_Appuser>()[0];
                _password = Base64Decode(t_Appuser.Password);
            }
            catch (SQLiteException sqlex)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", "Ocurrió un error.", "Aceptar");
                return;
            }

            if (string.CompareOrdinal(this.Password, this._password) != 0)
            {
                //this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Clave inválida.", "Aceptar");
                this.Password = string.Empty;
                return;
            }

            this.IsEnabled = true;
            this.Password = string.Empty;

            MainViewModel.GetInstance().HomePageDetail = new HomePageDetailViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
        #endregion

        #region Helpers
        private static IEnumerable<T_Appuser> SELECT_WHERE(SQLiteConnection db, string name)
        {
            return db.Query<T_Appuser>("SELECT * FROM T_Appuser where Name = ?", name);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodeBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }
        #endregion
    }
}
