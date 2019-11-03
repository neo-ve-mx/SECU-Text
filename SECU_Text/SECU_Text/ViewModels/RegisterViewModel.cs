using GalaSoft.MvvmLight.Command;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Atributes
        private string user;
        private string password;
        private bool isrunning;
        private bool isenabled;
        #endregion

        #region Properties
        public string User
        {
            get { return user; }
            set { SetValue(ref user, value); }
        }
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
            if (string.IsNullOrEmpty(this.User))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Ingrese su usuario.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Ingrese su contraseña.",
                    "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            Thread.Sleep(4000);

            this.IsRunning = false;
            this.IsEnabled = true;

            //if (string.CompareOrdinal(this.Usuario, "ADMINISTRADOR") != 0 || string.CompareOrdinal(this.Clave, "Accusys123*") != 0)
            //{
            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    await Application.Current.MainPage.DisplayAlert(
            //        "Error",
            //        "Usuario y/o clave inválidas.",
            //        "Aceptar");
            //    this.Usuario = string.Empty;
            //    this.Clave = string.Empty;
            //    return;
            //}

            //this.IsRunning = false;
            //this.IsEnabled = true;
            ////await Application.Current.MainPage.DisplayAlert(
            ////    "Correcto",
            ////    "Acceso concedido.",
            ////    "Aceptar");
            ////return;

            //this.Usuario = string.Empty;
            //this.Clave = string.Empty;

            //MainViewModel.ObtenerInstancia().Inicio = new InicioViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new InicioPage());
        }
        #endregion
    }
}
