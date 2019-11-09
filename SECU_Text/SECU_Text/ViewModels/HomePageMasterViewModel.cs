using SECU_Text.Models;
using SECU_Text.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SECU_Text.ViewModels
{
    public class HomePageMasterViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<HomePageMasterMenuItem> menuitems;
        #endregion

        #region Properties
        public ObservableCollection<HomePageMasterMenuItem> MenuItems 
        {
            get { return menuitems; }
            set { SetValue(ref menuitems, value); }
        }
        #endregion

        #region Constructors
        public HomePageMasterViewModel()
        {
            MenuItems = new ObservableCollection<HomePageMasterMenuItem>(new[]
                {
                    new HomePageMasterMenuItem { Id = 0, Icon = "list_icon", Title = "Inicio", TargetType = typeof(HomePage)  },
                    new HomePageMasterMenuItem { Id = 1, Icon = "security_icon", Title = "Seguridad", TargetType = typeof(SecurityPage)  },
                    new HomePageMasterMenuItem { Id = 2, Icon = "config_icon", Title = "Opciones", TargetType = typeof(OptionsPage) },
                    new HomePageMasterMenuItem { Id = 3, Icon = "about_icon", Title = "Acerca", TargetType = typeof(AboutPage) }
                });
        }
        #endregion
    }
}
