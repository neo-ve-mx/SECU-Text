using SECU_Text.Models;
using SECU_Text.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public RegisterViewModel Register { get; set; }
        public HomePageViewModel HomePage { get; set; }
        public HomePageMasterViewModel HomePageMaster { get; set; }
        public HomePageDetailViewModel HomePageDetail { get; set; }
        public AddItemViewModel AddItem { get; set; }
        public ViewItemViewModel ViewItem { get; set; }
        public EditItemViewModel EditItem { get; set; }
        public SecurityViewModel Security { get; set; }
        public OptionsViewModel Options { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            SQLiteConnection db = platform.GetConnection();

            try
            {
                #region Verify Tables
                var resultTitem = db.GetTableInfo("T_Entry");
                if (resultTitem.Count == 0)
                {
                    db.CreateTable<T_Entry>();
                }
                var resultTappuser = db.GetTableInfo("T_Appuser");
                if (resultTappuser.Count == 0)
                {
                    db.CreateTable<T_Appuser>();
                }
                var resultTconfig = db.GetTableInfo("T_Config");
                if (resultTconfig.Count == 0)
                {
                    db.CreateTable<T_Config>();
                    T_Config t_Config = new T_Config { ItemsOrder = "IconTitle", FingerPrintAllow = false };
                    var resultDB = db.Insert(t_Config);
                    if (resultDB != 1)
                    {
                        throw new Exception("No se pudo crear la configuración inicial.");
                    }
                }
                #endregion
            }
            catch (SQLiteException sqlex)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", "Ocurrió un error.", "Aceptar");
            }

            instance = this;
            this.Login = new LoginViewModel();
            this.Register = new RegisterViewModel();
            this.HomePage = new HomePageViewModel();
            this.HomePageMaster = new HomePageMasterViewModel();
            this.HomePageDetail = new HomePageDetailViewModel();
            this.AddItem = new AddItemViewModel();
            this.ViewItem = new ViewItemViewModel(new Models.T_Entry());
            this.EditItem = new EditItemViewModel(new Models.T_Entry());
            this.Security = new SecurityViewModel();
            this.Options = new OptionsViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
