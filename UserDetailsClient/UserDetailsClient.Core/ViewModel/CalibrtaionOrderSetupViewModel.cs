using ADASMobileClient.Core.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;


namespace ADASMobileClient.Core.ViewModel
{
    public class CalibrtaionOrderSetupViewModel
    {

        HttpClient client;
        public static string LocalBaseUrl = "https:// 172.30.166.161:20300";
        private  ObservableCollection<WorkOrderModel> CalibrationOrderSetUp { get;  set; }
               

        public CalibrtaionOrderSetupViewModel()
        {
            GetdataBinding();
        }

        private async void GetdataBinding()
        {
            var httpClientHandler = new HttpClientHandler();
            CalibrationOrderSetUp = new ObservableCollection<WorkOrderModel>();
            client = new HttpClient();
            var httpClientHandler1 = new HttpClientHandler();
            CalibrationOrderSetUp = new ObservableCollection<WorkOrderModel>();
            client = new HttpClient();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/text"));

                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //specify to use TLS 1.2 as default connection
                var getResult = await client.GetAsync(LocalBaseUrl + "/api/entity/workorder/all");
                if (getResult.IsSuccessStatusCode)
                {

                    var response = await getResult.Content.ReadAsStringAsync();


                    var reqMonkeys = JsonConvert.DeserializeObject<List<WorkOrderModel>>(response);

                    var TotalWorkOrderItem = reqMonkeys.Count;
                   // MultiSelectListView=reqMonkeys.
                }
            }
            catch (Exception ex) {
                Debug.WriteLine("Exception Error ", ex.ToString());
            }
                }
    }
}
