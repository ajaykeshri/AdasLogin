using ADASMobileClient.Core.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UserDetailsClient.Core.model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UserDetailsClient.Core
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VehicalDetail : ContentPage
	{

        HttpClient client;
        VehicleVinResponse vehicleVinResponse;
        private string token;

       
        public VehicalDetail ()
		{
			InitializeComponent ();
            stackDisplay.SetValue(IsVisibleProperty, false);
            StackVinDisplay.SetValue(IsVisibleProperty, false);
            stackVinDisplay.SetValue(IsVisibleProperty, false);
            actIndicator2.IsRunning = false;
            StackVinDisplay.SetValue(IsVisibleProperty, false);

        }

     

        private async void Submitt_Clicked(object sender, EventArgs e)
        {

            actIndicator2.IsRunning = true;
            // Check network status  
            if (NetworkCheck.IsInternet())
            {
                client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // var authHeader = new AuthenticationHeaderValue("bearer", "accessToken.Token");  // pass token 
                //  client.DefaultRequestHeaders.Authorization = authHeader;

                var enteredValues = orderNumber.Text;
                var formContent = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("ordernum", enteredValues),
                });

                try { 
                       var result = await client.PostAsync("http://172.16.204.41:7000/getVehicle", formContent);
                    //var result = await client.PostAsync("https://172.16.204.41:20300/api/vehicle/vindecode/2T1BR32EX6C593681", formContent);
                    var responseResult = await result.Content.ReadAsStringAsync();
              
                VehicleResponse vehicleResponse = JsonConvert.DeserializeObject<VehicleResponse>(responseResult);
               
               
                // on error throw a exception  
                result.EnsureSuccessStatusCode();


                if (result.IsSuccessStatusCode)
                {
                    actIndicator2.IsRunning = false;
                    stackDisplay.SetValue(IsVisibleProperty, true);
                    VehicleName.Text = vehicleResponse.name;
                    stackForm.SetValue(IsVisibleProperty, false);
                    StackVinDisplay.SetValue(IsVisibleProperty, false);
                    stackVinDisplay.SetValue(IsVisibleProperty, true);

                    CarNumberDisplace.Text = "Work Order # " + enteredValues;
                }
                else
                {
                    actIndicator2.IsRunning = false;
                    await DisplayAlert("Network Problem", "Please check interent connection or server runing status", "OK");
                }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine( " Exception error Order number API ",ex.ToString());
                }

            }
            else
            {
                actIndicator2.IsRunning = false;
                await DisplayAlert("Network Problem", "Please check interent connection or server runing status", "OK");
            }
        }

     

        private async void Button_ClickedVin(object sender, EventArgs e)
        {
            
            stackDisplay.SetValue(IsVisibleProperty, false);
            stackForm.SetValue(IsVisibleProperty, false);
            
            actIndicator2.IsRunning = true;
           

            var httpClientHandler = new HttpClientHandler();
               client = new HttpClient();
           

            if (Application.Current.Properties.ContainsKey("token"))
            {
              //  LabMessage.Text = Application.Current.Properties["token"].ToString();
                token = Application.Current.Properties["token"].ToString();
                Debug.WriteLine("TokenPass from Azure", token);


                var VinNumber = "2T1BR32EX6C593681";//VinEnteredNumber.Text;

                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                   
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //specify to use TLS 1.2 as default connection
                        var getResult = await client.GetAsync("https://172.16.204.41:20300/api/vehicle/vindecode/" + VinNumber);
                    if (getResult.IsSuccessStatusCode)
                    {
                        StackVinDisplay.SetValue(IsVisibleProperty, true);
                        actIndicator2.IsRunning = false;
                        var response = await getResult.Content.ReadAsStringAsync();

                        VehicleVinResponse vehicleVinResponse = JsonConvert.DeserializeObject<VehicleVinResponse>(response);
                        LabCount.Text = Convert.ToString(vehicleVinResponse.Count);
                        LabMessage.Text = vehicleVinResponse.Message;
                        LabSearchCriteria.Text = vehicleVinResponse.SearchCriteria;

                        //LabValue.Text = vehicleVinResponse.Results[6].Value;
                        //LabIdValue.Text = vehicleVinResponse.Results[6].ValueId;
                        //LabVariable.Text = vehicleVinResponse.Results[6].Variable;
                        //LabVariableId.Text = Convert.ToString(vehicleVinResponse.Results[6].VariableId);


                        VinResult.ItemsSource = vehicleVinResponse.Results;
                        getResult.EnsureSuccessStatusCode();
                        //throw new Exception("Oh there is exception");
                    }
                    else
                    {
                        actIndicator2.IsRunning = false;
                        await DisplayAlert("API error", "Please check API", "OK");
                    }
                }
                catch (Exception  ex)
                {
                    actIndicator2.IsRunning = false;
                    // Log Error.
                    LabCount.Text =
                        "I'm sorry, but I couldn't load the page," +
                        " possibly due to network problems." +
                        "Here's the error message I received: "
                        + ex.ToString();
                    Debug.WriteLine("Exception Error " ,ex.ToString());
                }


            }






        }
    }
}