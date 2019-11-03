using GalaSoft.MvvmLight.Command;
using SECU_Text.Views;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class LoginViewModel : BaseViewModel
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
        public LoginViewModel()
        {
            this.IsEnabled = true;

            this.User = "NEOVEMX";
            this.Password = "n30v3mx";
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

            //this.IsRunning = true;
            this.IsEnabled = false;
            //Thread.Sleep(5000);

            if (string.CompareOrdinal(this.User, "NEOVEMX") != 0 || string.CompareOrdinal(this.Password, "n30v3mx") != 0)
            {
                //this.IsRunning = false;
                //this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Usuario y/o clave inválidas.",
                    "Aceptar");
                this.User = string.Empty;
                this.Password = string.Empty;
                return;
            }

            this.IsEnabled = true;

            this.User = string.Empty;
            this.Password = string.Empty;

            MainViewModel.GetInstance().Home = new HomeViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
        #endregion
    }
}
