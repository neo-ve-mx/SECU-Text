using System;
using System.Collections.Generic;
using System.Text;

namespace SECU_Text.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }

        public RegisterViewModel Register { get; set; }

        public HomeViewModel Home { get; set; }
        public AddItemViewModel AddItem { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.Register = new RegisterViewModel();
            this.Home = new HomeViewModel();
            this.AddItem = new AddItemViewModel();
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
