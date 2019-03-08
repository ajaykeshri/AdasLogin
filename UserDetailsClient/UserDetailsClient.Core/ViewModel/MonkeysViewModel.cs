using ADASMobileClient.Core.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ADASMobileClient.Core.ViewModel
{
    class MonkeysViewModel
    {
        HttpClient client;
        public static string LocalBaseUrl = "https://192.168.0.102:20300";
        public ObservableCollection<WorkOrderModel> Monkeys { get; private set; }
        public IList<WorkOrderModel> EmptyMonkeys { get; private set; }

        public MonkeysViewModel()
        {
            GetdataBinding();
          

            
        }

        private async void GetdataBinding()
        {
            var httpClientHandler = new HttpClientHandler();
            Monkeys = new ObservableCollection<WorkOrderModel>();
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
                   // Monkeys = new List<WorkOrderModel>();
                    //int i = 3;
                    for (int i=0; i< TotalWorkOrderItem;i++)
                    {
                       
                        Monkeys.Add(new WorkOrderModel() { vinnumber = reqMonkeys[i].vinnumber, workorder = reqMonkeys[i].workorder,
                            model = reqMonkeys[i].model,  startdate = reqMonkeys[i].startdate

                        ,
                            enddate = reqMonkeys[i].enddate

                        ,
                            totalcalibration = reqMonkeys[i].totalcalibration

                        ,
                            numberofcalibrationcompleted = reqMonkeys[i].numberofcalibrationcompleted
                                                        
                            ,
                            totalactiveworkorder = reqMonkeys[i].totalactiveworkorder
                            ,
                            createdby = reqMonkeys[i].createdby,

                            createddate = reqMonkeys[i].createddate,
                            lastupdatedby = reqMonkeys[i].lastupdatedby,
                            lastupdateddate = reqMonkeys[i].lastupdateddate
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