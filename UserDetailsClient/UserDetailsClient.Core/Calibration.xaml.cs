using System;
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
using ADASMobileClient.Core.model;
using UserDetailsClient.Core;

namespace ADASMobileClient.Core
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calibration : ContentPage
	{
        HttpClient client;
        VehicleVinResponse vehicleVinResponse;
        private string token;
        string BaseUrl = "https://vsgdev.centralus.cloudapp.azure.com:20300";

        public Calibration ()
		{
			InitializeComponent ();
            StackVinDisplay.SetValue(IsVisibleProperty, false);
            CalibrationResult.SetValue(IsVisibleProperty, false);
           
            vinEnterLayout.SetValue(IsVisibleProperty, true);
            actIndicator2.IsRunning = false;
            VehicleDetailsLabel.SetValue(IsVisibleProperty, false);


        }


        private async void Submitt_Clicked(object sender, EventArgs e)
        {
           
          
            actIndicator2.IsRunning = true;
            StackVinDisplay.SetValue(IsVisibleProperty, false);


            var httpClientHandler = new HttpClientHandler();
            client = new HttpClient();


            if (Application.Current.Properties.ContainsKey("token"))
            {
                //  LabMessage.Text = Application.Current.Properties["token"].ToString();
                token = Application.Current.Properties["token"].ToString();
                Debug.WriteLine("TokenPass from Azure", token);


                var VinNumber = VinEnteredNumber.Text;

                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //specify to use TLS 1.2 as default connection
                    var getResult = await client.GetAsync(BaseUrl + "/api/vehicle/vindecode/" + VinNumber);
                    if (getResult.IsSuccessStatusCode)
                    {
                        StackVinDisplay.SetValue(IsVisibleProperty, false);
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
                        CarName.Text = vehicleVinResponse.Results[6].Value;
                        vinEnterLayout.SetValue(IsVisibleProperty, false);
                        CalibrationResult.SetValue(IsVisibleProperty, true);
                        VehicleDetailsLabel.SetValue(IsVisibleProperty, true);
                        vinNumber.Text = VinNumber;
                        workOrder.Text = vehicleVinResponse.Results[6].ValueId;
                        brandName.Text =vehicleVinResponse.Results[6].Value;
                        modelNumber.Text =Convert.ToString(vehicleVinResponse.Results[6].VariableId);
                      //  platNumber.Text = vehicleVinResponse.Results[6].ValueId;
                        year.Text = "Year : " + "2016";

                         

                        VinResult.ItemsSource = vehicleVinResponse.Results;
                        getResult.EnsureSuccessStatusCode();
                        //throw new Exception("Oh there is exception");
                    }
                    else
                    {
                        VehicleDetailsLabel.SetValue(IsVisibleProperty, false);
                        actIndicator2.IsRunning = false;
                        CalibrationResult.SetValue(IsVisibleProperty, false);
                        await DisplayAlert("API error", "Please check API", "OK");
                    }
                }
                catch (Exception ex)
                {
                    actIndicator2.IsRunning = false;
                    // Log Error.
                    LabCount.Text =
                        "I'm sorry, but I couldn't load the page," +
                        " possibly due to network problems." +
                        "Here's the error message I received: "
                        + ex.ToString();
                    Debug.WriteLine("Exception Error ", ex.ToString());
                }


            }


        }

        private void ConfirmCalibrationCancel_Clicked(object sender, EventArgs e)
        {
            vinEnterLayout.SetValue(IsVisibleProperty, true);
            CalibrationResult.SetValue(IsVisibleProperty, false);
            VehicleDetailsLabel.SetValue(IsVisibleProperty, false);
        }

       
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Calibration());
        }

        private async void ConfirmCalibrationSubmitt_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new GrideListPage());


        }

    }
}