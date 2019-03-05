
using Android.App;
using Android.Content.PM;
using Microsoft.Identity.Client;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using UserDetailsClient.Core;
using Xamarin.Forms.Platform.Android;
using System.Net;

namespace UserDetailsClient.Droid
{
    //, Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
    [Activity(Label = "ADASMobileClient", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
          

            TabLayoutResource = ADASMobileClient.Droid.Resource.Layout.Tabbar;
            ToolbarResource = ADASMobileClient.Droid.Resource.Layout.Toolbar;
          
            base.OnCreate(bundle);
            // You may use ServicePointManager here
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
           // global::Xamarin.Forms.Forms.SetFlags(new[] { "CollectionView_Experimental", "Shell_Experimental" });
            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
            App.UiParent = new UIParent(this);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }

       
    }
}

