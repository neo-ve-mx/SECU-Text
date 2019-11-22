using SECU_Text.Droid.Implementations;
using SECU_Text.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]

namespace SECU_Text.Droid.Implementations
{
    public class Toast_Android : Toast
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, Android.Widget.ToastLength.Long).Show();
        }
    }
}