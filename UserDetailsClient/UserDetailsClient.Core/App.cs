using System;
using ADASMobileClient.Core;
using ADASMobileClient.Core.view;
using Microsoft.Identity.Client;

using Xamarin.Forms;

namespace UserDetailsClient.Core
{
    public class App : Application
    {
        public static PublicClientApplication PCA = null;

        // Azure AD B2C Coordinates
        public static string Tenant = "dovervsg.onmicrosoft.com";
        public static string ClientID = "ef7a9bba-33eb-42e5-b010-eb383a9773b8";
        public static string PolicySignUpSignIn = "B2C_1_vsg_sign-in-policy";
        public static string PolicyEditProfile = "B2C_1_edit_policy";
        public static string PolicyResetPassword = "B2C_1_reset_password";

        public static string[] Scopes = { "User.Read" };
        public static string Username = string.Empty;

        public static UIParent UiParent { get; set; }


        public static string AuthorityBase = $"https://login.microsoftonline.com/tfp/{Tenant}/";
        public static string Authority = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityPasswordReset = $"{AuthorityBase}{PolicyResetPassword}";

       

        //  public static string BaseUrl = "https://172.16.204.41:20300"; //office
        // public static string BaseUrl = "https://172.30.166.161:20300";    //jeo
        public static string BaseUrl = "https://vsgdev.centralus.cloudapp.azure.com:20300";
       // public static string BaseUrlLocal = "https://192.168.0.103:20300";

       

        public App()
        {
            
            // default redirectURI; each platform specific project will have to override it with its own
            PCA = new PublicClientApplication(ClientID)
            {
                RedirectUri = $"msal{App.ClientID}://auth",
            };

            //  MainPage = new NavigationPage(new SplashPage());
            // MainPage = new NavigationPage(new VehicalDetail());
            //  MainPage = new NavigationPage(new VerticalGridPage());
            // MainPage = new NavigationPage(new CalibrationOrderSetupPage());
            //   MainPage = new NavigationPage(new GrideListPage());
                MainPage = new NavigationPage(new ScanPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
