using GalaSoft.MvvmLight.Command;
using SECU_Text.Views;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class HomeViewModel
    {
        #region Atributes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public HomeViewModel()
        {

        }
        #endregion

        #region Commands
        public ICommand NewItemCommand
        {
            get
            {
                return new RelayCommand(NewItem);
            }
        }

        private async void NewItem()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddItemPage());
        }
        #endregion
    }
}
