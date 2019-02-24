using System;
using ADASMobileClient.Core;
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

        public static string[] Scopes = { "https://dovervsg.onmicrosoft.com/demoapi/read" };


        public static string AuthorityBase = $"https://login.microsoftonline.com/tfp/{Tenant}/";
        public static string Authority = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityPasswordReset = $"{AuthorityBase}{PolicyResetPassword}";

        public static UIParent UiParent = null;
      //  public static string BaseUrl = "https://172.16.204.41:20300";
        public static string BaseUrl = "https://172.30.166.161:20300"; 



        public App()
        {
            // default redirectURI; each platform specific project will have to override it with its own
            PCA = new PublicClientApplication(ClientID, Authority);
            PCA.RedirectUri = $"msal{ClientID}://auth";

             MainPage = new NavigationPage(new SplashPage());
            // MainPage = new NavigationPage(new VehicalDetail());
           // MainPage = new NavigationPage(new Example());

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
