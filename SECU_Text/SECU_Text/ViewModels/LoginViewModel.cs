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
using Plugin.Fingerprint;

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
        private bool passwordisvisible;
        private bool fingerprintisvisible;
        private bool fpisavailable;
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

        public bool PasswordIsVisible
        {
            get { return passwordisvisible; }
            set { SetValue(ref passwordisvisible, value); }
        }

        public bool FingerPrintIsVisible
        {
            get { return fingerprintisvisible; }
            set { SetValue(ref fingerprintisvisible, value); }
        }
        #endregion

        #region Constructores
        public LoginViewModel()
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            db = platform.GetConnection();

            this.IsEnabled = true;

            //this.Password = "n30v3mx";

            try
            {
                GetFingerPrintAvailable();
                if (fpisavailable)
                {
                    IEnumerable<T_Config> getConfigApp = GetConfigApp(db);
                    T_Config t_Config = getConfigApp.ToList<T_Config>()[0];
                    if (t_Config.FingerPrintAllow)
                    {
                        this.PasswordIsVisible = false;
                        this.FingerPrintIsVisible = true;
                    }
                    else
                    {
                        this.PasswordIsVisible = true;
                        this.FingerPrintIsVisible = false;
                    }
                }
                else
                {
                    IEnumerable<T_Config> getConfigApp = GetConfigApp(db);
                    T_Config t_Config = getConfigApp.ToList<T_Config>()[0];

                    if (t_Config.FingerPrintAllow)
                    {
                        var changeData = new T_Config { Id = t_Config.Id, ItemsOrder = t_Config.ItemsOrder, FingerPrintAllow = false };
                        var resultDB = db.Update(changeData);
                    }
                    this.PasswordIsVisible = true;
                    this.FingerPrintIsVisible = false;
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
        public ICommand FingerCommand
        {
            get
            {
                return new RelayCommand(Finger);
            }
        }

        private async void Finger()
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync("Toque el sesor de huellas...");
            if (result.Authenticated)
            {
                MainViewModel.GetInstance().HomePageDetail = new HomePageDetailViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                this.PasswordIsVisible = true;
                this.FingerPrintIsVisible = false;
            }
        }

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
                IEnumerable<T_Appuser> getAppUser = GetAppUser(db, "ApplicationUser");
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
        private static IEnumerable<T_Appuser> GetAppUser(SQLiteConnection db, string name)
        {
            return db.Query<T_Appuser>("SELECT * FROM T_Appuser where Name = ?", name);
        }

        private static IEnumerable<T_Config> GetConfigApp(SQLiteConnection db)
        {
            return db.Query<T_Config>("SELECT * FROM T_Config where Id = 1");
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodeBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }

        private async void GetFingerPrintAvailable()
        {
            fpisavailable = await CrossFingerprint.Current.IsAvailableAsync();
        }
        #endregion
    }
}
