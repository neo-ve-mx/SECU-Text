using GalaSoft.MvvmLight.Command;
using Plugin.Fingerprint;
using SECU_Text.Models;
using SECU_Text.Services;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using SECU_Text.Helpers;

namespace SECU_Text.ViewModels
{
    public class SecurityViewModel : BaseViewModel
    {
        #region Atributes
        private SQLiteConnection db;
        private string password;
        private bool swistoggled;
        private bool swisenabled;
        private bool isenabled;
        private bool scisenabled;
        private bool fpisavailable;
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get { return isenabled; }
            set { SetValue(ref isenabled, value); }
        }
        public string Password
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }
        public bool SwIsToggled
        {
            get { return swistoggled; }
            set { SetValue(ref swistoggled, value); }
        }
        public bool SwIsEnabled
        {
            get { return swisenabled; }
            set { SetValue(ref swisenabled, value); }
        }

        public bool ScIsEnabled
        {
            get { return scisenabled; }
            set { SetValue(ref scisenabled, value); }
        }

        public T_Appuser t_Appuser { get; set; }
        public T_Config t_Config { get; set; }
        #endregion

        #region Constructors
        public SecurityViewModel()
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            db = platform.GetConnection();

            IsEnabled = true;

            try
            {
                IEnumerable<T_Appuser> getAppUser = GetAppUser(db, "ApplicationUser");
                if (getAppUser.ToList<T_Appuser>().Count != 0)
                {
                    t_Appuser = getAppUser.ToList<T_Appuser>()[0];
                }
                GetFingerPrintAvailable();
                if (fpisavailable)
                {
                    IEnumerable<T_Config> getConfigApp = GetConfigApp(db);
                    t_Config = getConfigApp.ToList<T_Config>()[0];
                    if (t_Config.FingerPrintAllow)
                    {
                        ScIsEnabled = true;
                        SwIsEnabled = true;
                        SwIsToggled = true;
                    }
                    else
                    {
                        ScIsEnabled = true;
                        SwIsEnabled = true;
                        SwIsToggled = false;
                    }
                }
                else
                {
                    SwIsToggled = false;
                    SwIsEnabled = false;
                    ScIsEnabled = false;
                }
            }
            catch (SQLiteException sqlex)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1, 
                    Languages.ExceptionLiteral2, 
                    Languages.ExceptionLiteral3);
                return;
            }
        }
        #endregion

        #region Commands
        public ICommand ChangeCommand
        {
            get
            {
                return new RelayCommand(Change);
            }
        }

        private async void Change()
        {
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1, 
                    Languages.SecurityLiteral1, 
                    Languages.ExceptionLiteral3);
                return;
            }

            this.IsEnabled = false;
            try
            {
                var changeData = new T_Appuser { Id = t_Appuser.Id, Name = "ApplicationUser", Password = Base64Encode(Password) };
                var resultDB = db.Update(changeData);
                if (resultDB == 1)
                {
                    Password = string.Empty;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.AppLiteral1, 
                        Languages.SecurityLiteral2, 
                        Languages.ExceptionLiteral3);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.ExceptionLiteral1, 
                        Languages.SecurityLiteral3, 
                        Languages.ExceptionLiteral3);
                    return;
                }
            }
            catch (SQLiteException sqlex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1, 
                    Languages.ExceptionLiteral2, 
                    Languages.ExceptionLiteral3);
                return;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            this.ScIsEnabled = false;
            try
            {
                var changeData = new T_Config { Id = t_Config.Id, ItemsOrder = t_Config.ItemsOrder, FingerPrintAllow = SwIsToggled };
                var resultDB = db.Update(changeData);
                if (resultDB == 1)
                {
                    this.ScIsEnabled = true;
                    if (SwIsToggled)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.AppLiteral1, 
                            Languages.SecurityLiteral4, 
                            Languages.ExceptionLiteral3);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.AppLiteral1, 
                            Languages.SecurityLiteral5, 
                            Languages.ExceptionLiteral3);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.ExceptionLiteral1, 
                        Languages.SecurityLiteral6, 
                        Languages.ExceptionLiteral3);
                    return;
                }
            }
            catch (SQLiteException sqlex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1, 
                    Languages.ExceptionLiteral2, 
                    Languages.ExceptionLiteral3);
                return;
            }
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

        private static string Base64Encode(string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private async void GetFingerPrintAvailable()
        {
            fpisavailable = await CrossFingerprint.Current.IsAvailableAsync();
        }
        #endregion
    }
}
