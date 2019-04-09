using ADASMobileClient.Core.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace ADASMobileClient.Core.ViewModel
{
     class WorkOrderViewModel
    {
        HttpClient client;
        private string token;

        public ObservableCollection<WorkOrderModel> WorkOrderModelList { get; private set; }
        string BaseLocalHost = "https://192.168.0.102:20300";



        public WorkOrderViewModel()
        {
            GetdataBinding();



        }

        private async void GetdataBinding()
        {
            var httpClientHandler = new HttpClientHandler();
            WorkOrderModelList = new ObservableCollection<WorkOrderModel>();
            client = new HttpClient();
            token = Application.Current.Properties["token"].ToString();
           // Debug.WriteLine("TokenPass from Azure", token);
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

               // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/text"));

                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //specify to use TLS 1.2 as default connection

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
               // var getResult = await client.GetAsync(Constants.BaseUrlLocal + "/api/entity/workorder/all");
                var getResult = await client.GetAsync(Constants.LocalHost + "/api/entity/workorder/all");
               
                if (getResult.IsSuccessStatusCode)
                {
                    
                    var response = await getResult.Content.ReadAsStringAsync();


                    var reqWorkModel = JsonConvert.DeserializeObject<List<WorkOrderModel>>(response);

                    var TotalWorkOrderItem = reqWorkModel.Count;
                   
                    for (int i = 0; i < TotalWorkOrderItem; i++)
                    {

                        WorkOrderModelList.Add(new WorkOrderModel()
                        {
                            vinnumber = reqWorkModel[i].vinnumber,
                            workorder = reqWorkModel[i].workorder,
                            model = reqWorkModel[i].model,
                            startdate = reqWorkModel[i].startdate ,
                            enddate = reqWorkModel[i].enddate ,
                            totalcalibration = reqWorkModel[i].totalcalibration,
                            numberofcalibrationcompleted = reqWorkModel[i].numberofcalibrationcompleted,
                            totalactiveworkorder = reqWorkModel[i].totalactiveworkorder,
                            createdby = reqWorkModel[i].createdby,
                            createddate = reqWorkModel[i].createddate,
                            lastupdatedby = reqWorkModel[i].lastupdatedby,
                            lastupdateddate = reqWorkModel[i].lastupdateddate
                        });

                    }



                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Error ", ex.ToString());
            }




        }
    }
}
