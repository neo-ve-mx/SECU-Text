using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SECU_Text.Helpers;
using SECU_Text.Interfaces;

namespace SECU_Text.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageDetail : ContentPage
    {
        #region Atributes
        public ListView ListViewDetailPage;
        public int countdown = 3;
        #endregion

        #region Properties
        public int CountDown
        {
            get
            {
                return countdown;
            }
            set
            {
                countdown = value;
            }
        }
        public DateTime TimeCountDown { get; set; }
        #endregion

        public HomePageDetail()
        {
            InitializeComponent();
            ListViewDetailPage = EntryItemsListView;
        }

        protected override bool OnBackButtonPressed()
        {
            TimeCountDown = DateTime.Now;
            if (DateTime.Now.Minute > TimeCountDown.Minute)
            {
                CountDown = 3;
            }
            CountDown -= 1;
            if (CountDown == 0)
            {
                return false;
            }
            else
            {
                if (CountDown > 0)
                {
                    DependencyService.Get<Toast>().Show(Languages.AppLiteral4 + " " + CountDown + " " + Languages.AppLiteral5);
                }
            }
            return true;
        }
    }
}