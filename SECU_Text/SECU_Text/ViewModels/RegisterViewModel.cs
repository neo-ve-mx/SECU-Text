using GalaSoft.MvvmLight.Command;
using SECU_Text.Models;
using SECU_Text.Services;
using SECU_Text.Views;
using SQLite;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Atributes
        private SQLiteConnection db;
        private string password;
        private bool isrunning;
        private bool isenabled;
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
        public RegisterViewModel()
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            db = platform.GetConnection();

            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
        {
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Ingrese su contraseña.",
                    "Aceptar");
                return;
            }

            //this.IsRunning = true;
            this.IsEnabled = false;

            try
            {
                var registerData = new T_Appuser { Name = "ApplicationUser", Password = Base64Encode(Password) };
                var resultDB = db.Insert(registerData);
                if (resultDB == 1)
                {
                    //this.IsRunning = false;
                    Password = string.Empty;
                    this.IsEnabled = true;
                    MainViewModel.GetInstance().Register = new RegisterViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("ERROR", "No se pudo crear la clave.", "Aceptar");
                    return;
                }
            }
            catch (SQLiteException sqlex)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", "Ocurrió un error.", "Aceptar");
                return;
            }
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
