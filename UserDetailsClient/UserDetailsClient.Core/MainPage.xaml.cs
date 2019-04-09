﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using ADASMobileClient.Core;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace UserDetailsClient.Core
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            actIndicator2.IsRunning = false;
        }

        async void OnSignInSignOut(object sender, EventArgs e)
        {
            actIndicator2.IsRunning = true;
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await App.PCA.GetAccountsAsync();
            try
            {
                if (btnSignInSignOut.Text == "Sign in")
                {
                    // let's see if we have a user in our belly already
                    try
                    {
                        IAccount firstAccount = accounts.FirstOrDefault();
                        authResult = await App.PCA.AcquireTokenSilentAsync(App.Scopes, firstAccount);
                       // await Navigation.PushAsync(new GrideListPage());
                        await RefreshUserDataAsync(authResult.AccessToken).ConfigureAwait(false);
                     
                        Device.BeginInvokeOnMainThread(() => { btnSignInSignOut.Text = "Sign out"; });
                        actIndicator2.IsRunning = false;
                    }
                    catch (MsalUiRequiredException ex)
                    {
                        authResult = await App.PCA.AcquireTokenAsync(App.Scopes, App.UiParent);
                        await RefreshUserDataAsync(authResult.AccessToken);
                        Device.BeginInvokeOnMainThread(() => { btnSignInSignOut.Text = "Sign out"; });
                        actIndicator2.IsRunning = false;
                    }
                }
                else
                {
                    while (accounts.Any())
                    {
                        actIndicator2.IsRunning = true;
                        await App.PCA.RemoveAsync(accounts.FirstOrDefault());
                        accounts = await App.PCA.GetAccountsAsync();
                        await Navigation.PushAsync(new GrideListPage());
                        actIndicator2.IsRunning = false;
                    }

                    // slUser.IsVisible = false;
                  
                    Device.BeginInvokeOnMainThread(() => { btnSignInSignOut.Text = "Sign in"; });
                    actIndicator2.IsRunning = true;
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task RefreshUserDataAsync(string token)
        {
            //get data from API
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await client.SendAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                JObject user = JObject.Parse(responseString);

                //   slUser.IsVisible = true;
               //await Navigation.PushAsync(new GrideListPage());

                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.Properties["token"] = token;// user["oid"]?.ToString();
                    Navigation.PushAsync(new GrideListPage());

                    //lblDisplayName.Text = user["displayName"].ToString();
                    //lblGivenName.Text = user["givenName"].ToString();
                    //lblId.Text = user["id"].ToString();
                    //lblSurname.Text = user["surname"].ToString();
                    //lblUserPrincipalName.Text = user["userPrincipalName"].ToString();
                   

                    // just in case
                    btnSignInSignOut.Text = "Sign out";
                    actIndicator2.IsRunning = false;
                });
            }
            else
            {
                await DisplayAlert("Something went wrong with the API call", responseString, "Dismiss");
            }
        }
    }
}