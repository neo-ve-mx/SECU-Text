using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Plugin.Fingerprint;

namespace SECU_Text.Droid
{
    [Activity(Label = "Secure Text", Icon = "@mipmap/icon", Theme = "@style/Theme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.MainTheme);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CrossFingerprint.SetCurrentActivityResolver(() => this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //public override void OnBackPressed()
        //{
        //    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        //    AlertDialog alert = dialog.Create();
        //    alert.SetTitle("CERRAR");
        //    alert.SetMessage("Deseas salir de la aplicación?");
        //    alert.SetButton("NO", (c, ev) =>
        //    {

        //    });
        //    alert.SetButton2("SI", (c, ev) =>
        //    {
        //        Finish();
        //    });
        //    alert.Show();
        //}
    }
}