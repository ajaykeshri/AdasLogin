using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ADASMobileClient.Core.model;
using ADASMobileClient.Core.view;

namespace ADASMobileClient.Core
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calibration : ContentPage
	{
        HttpClient client;
        VehicleVinResponse vehicleVinResponse;
        public List<CalibrationDetailRow> CalibrationDetailRowlList { get; private set; }

        private string token;
        
        string VinNumberScan;
       

        public Calibration (string vinNumber)
		{
			InitializeComponent ();
            StackVinDisplay.SetValue(IsVisibleProperty, false);
            CalibrationResult.SetValue(IsVisibleProperty, false);
           
            vinEnterLayout.SetValue(IsVisibleProperty, true);
            actIndicator2.IsRunning = false;
            VehicleDetailsLabel.SetValue(IsVisibleProperty, false);
            VinNumberScan = vinNumber;

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


                var VinNumber = VinEnteredNumber.Text ;
              //  var VinNumber =  VinNumberScan;

                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //specify to use TLS 1.2 as default connection
                    var getResult = await client.GetAsync(Constants.BaseUrlLocal + "/api/vehicle/vindecode/" + VinNumber);
                    if (getResult.IsSuccessStatusCode)
                    {
                        StackVinDisplay.SetValue(IsVisibleProperty, false);
                        actIndicator2.IsRunning = false;
                        var response = await getResult.Content.ReadAsStringAsync();

                                              
                        vehicleVinResponse = JsonConvert.DeserializeObject<VehicleVinResponse>(response);
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
                        brandName.Text = vehicleVinResponse.Results[6].Value;
                        modelNumber.Text = Convert.ToString(vehicleVinResponse.Results[6].VariableId);
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
            await Navigation.PushAsync(new GrideListPage());
        }

        private async void ConfirmCalibrationSubmitt_Clicked(object sender, EventArgs e)
        {
            actIndicator2.IsRunning = true;
            StackVinDisplay.SetValue(IsVisibleProperty, false);


            var httpClientHandler = new HttpClientHandler();
            client = new HttpClient();



            //  LabMessage.Text = Application.Current.Properties["token"].ToString();
            // token = Application.Current.Properties["token"].ToString();
            // Debug.WriteLine("TokenPass from Azure", token);




            var VinNumber = VinEnteredNumber.Text;
            WorkOrderModel workOrderModel = new WorkOrderModel();
            workOrderModel.id = VinNumber;
            workOrderModel.vinnumber = VinNumber;
            workOrderModel.workorder = vehicleVinResponse.Results[6].ValueId; ;
            workOrderModel.model = vehicleVinResponse.Results[6].Value;
            workOrderModel.startdate = "03032019";
            workOrderModel.enddate = "13032019";
            workOrderModel.totalcalibration = "2";
            workOrderModel.numberofcalibrationcompleted = "1";
            workOrderModel.totalactiveworkorder = "2";
            workOrderModel.iscompletedcalibration = true;
            workOrderModel.isactive = true;
            workOrderModel.createdby = "ADASAdmin";
            workOrderModel.createddate = "030319";
            workOrderModel.lastupdatedby = "040404";
            workOrderModel.lastupdateddate = "040404";
            workOrderModel.lastupdatedby = "040404";

           
            CalibrationDetailRowlList = new List<CalibrationDetailRow>();
            // CalibrationDetailRow calibrationDetailRows = new CalibrationDetailRow();
            CalibrationDetailRowlList.Add(new CalibrationDetailRow()
            {

                adasModuleName = "Module111",
                moduleAvailability = "Available",
                targetAvailability = "Standard",
                status = "Progress"



            });
            CalibrationDetailRowlList.Add(new CalibrationDetailRow()
            {

              adasModuleName = "Module3333",
             moduleAvailability = "Available",
             targetAvailability = "Standard",
             status = "Progress"
           


        });
            CalibrationDetailRowlList.Add(new CalibrationDetailRow()
            {

                adasModuleName = "Module44444",
                moduleAvailability = "Available",
                targetAvailability = "Standard",
                status = "Progress"



            });
            CalibrationDetailRowlList.Add(new CalibrationDetailRow()
            {

                adasModuleName = "Module555",
                moduleAvailability = "Available",
                targetAvailability = "Standard",
                status = "Progress"



            });

            workOrderModel.calibrationDetailRows = CalibrationDetailRowlList;



            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var json = JsonConvert.SerializeObject(workOrderModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(Constants.BaseUrlLocal + "/api/entity/workorder", content);


                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"                TodoItem successfully saved.");
                    actIndicator2.IsRunning = false;
                    await Navigation.PushAsync(new GrideListPage());



                }
                else
                {
                    Debug.WriteLine(@"     Faied to update data .");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Error ", ex.ToString());
            }



        }
        private void OnImageButtonVinClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScanPage());
        }
        private void OnImageButtonOrderNumberClicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new ScanPage());
        }
        private void OnImageButtonRepiarOrderNumberClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScanPage());
        }
        
    }
}