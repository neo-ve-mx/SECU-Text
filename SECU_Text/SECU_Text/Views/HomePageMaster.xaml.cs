using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SECU_Text.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;

        public HomePageMaster()
        {
            InitializeComponent();

            BindingContext = new HomePageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class HomePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomePageMasterMenuItem> MenuItems { get; set; }

            public HomePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomePageMasterMenuItem>(new[]
                {
                    new HomePageMasterMenuItem { Id = 0, Icon = "list_icon", Title = "Inicio", TargetType = typeof(HomePage)  },
                    new HomePageMasterMenuItem { Id = 1, Icon = "security_icon", Title = "Seguridad", TargetType = typeof(SecurityPage)  },
                    new HomePageMasterMenuItem { Id = 2, Icon = "config_icon", Title = "Opciones", TargetType = typeof(OptionsPage) },
                    new HomePageMasterMenuItem { Id = 3, Icon = "about_icon", Title = "Acerca de SECU-Text", TargetType = typeof(AboutPage) }
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}