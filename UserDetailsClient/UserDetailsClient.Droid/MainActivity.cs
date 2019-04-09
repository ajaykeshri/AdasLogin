
using Android.App;
using Android.Content.PM;
using Microsoft.Identity.Client;
using Android.Content;
using Android.OS;
using UserDetailsClient.Core;
using System.Net;
using Plugin.CurrentActivity;
using Plugin.Permissions;

//using Tesseract;





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
            // below code use for OCR and Bar code enable library
            Plugin.InputKit.Platforms.Droid.Config.Init(this, bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            try
            {
                LoadApplication(new App());
            }
            catch (System.Exception ex)
            {
                var thing = ex.Message;
            }

            // below line use for camera init
            CrossCurrentActivity.Current.Init(this, bundle);
            LoadApplication(new App());
            App.UiParent = new UIParent(this);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);

        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        //{
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

    }
}

