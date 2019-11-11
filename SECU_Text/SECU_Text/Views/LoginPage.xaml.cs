//using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SECU_Text.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        //protected async override void OnAppearing()
        //{
        //    var result = await CrossFingerprint.Current.AuthenticateAsync("Toque el sesor de huella...");
        //    if (result.Authenticated)
        //    {
        //        await DisplayAlert("Results are here", "Valid fingerprint found", "Ok");
        //    }
        //    else
        //    {
        //        await DisplayAlert("Results are here", "Invalid fingerprint", "Ok");
        //    }
        //}
    }
}