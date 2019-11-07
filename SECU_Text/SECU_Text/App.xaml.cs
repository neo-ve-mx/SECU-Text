using SECU_Text.Models;
using SECU_Text.Services;
using SECU_Text.Views;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SECU_Text
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var platform = DependencyService.Get<ISQLitePlatform>();
            SQLiteConnection db = platform.GetConnection();
            bool userRegistred = false;

            try
            {
                IEnumerable<T_Appuser> getAppUser = SELECT_WHERE(db, "ApplicationUser");
                if (getAppUser.ToList<T_Appuser>().Count != 0)
                {
                    userRegistred = true;
                }
                
            }
            catch (SQLiteException sqlex)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", "Ocurrió un error.", "Aceptar");
            }

            if (userRegistred)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }

            //MainPage.On<Xamarin.Forms.PlatformConfiguration.Windows>().SetToolbarPlacement(ToolbarPlacement.Bottom);
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

        private static IEnumerable<T_Appuser> SELECT_WHERE(SQLiteConnection db, string name)
        {
            return db.Query<T_Appuser>("SELECT * FROM T_Appuser where Name = ?", name);
        }
    }
}
