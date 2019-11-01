using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class LoginViewModel
    {
        #region Properties
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsRunning { get; set; }
        public bool IsEnabled { get; set; }
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
