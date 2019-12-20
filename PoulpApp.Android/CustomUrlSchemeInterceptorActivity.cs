using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using PoulpApp;

namespace PoulpApp.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "com.googleusercontent.apps.154307917586-0ehdaf5dblreni73b7371k0l0lllfvel" },
        DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Android.Net.Uri uri_android = Intent.Data;

            Uri uri_netfx = new Uri(uri_android.ToString());

            // load redirect_url Page
            AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

            this.Finish();
        }
    }
}