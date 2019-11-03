using SECU_Text.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SECU_Text
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // ELIMINAR LUEGO
            bool userRegistred = true;
            // ELIMINAR LUEGO

            if (userRegistred)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }

            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
