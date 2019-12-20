using Android.App;
using Android.Content.PM;
using Android.OS;

namespace PoulpApp.Droid
{
    [Activity(Label = "PoulpApp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", 
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);
            Xamarin.Auth.CustomTabsConfiguration.CustomTabsClosingMessage = null;

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}