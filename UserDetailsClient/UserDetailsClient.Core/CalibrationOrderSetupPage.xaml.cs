﻿using ADASMobileClient.Core.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADASMobileClient.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalibrationOrderSetupPage : ContentPage
    {
        private WorkOrderModel workOrderModel;
        private  ObservableCollection<CalibrationDetailRow> multiSelectListItems;
        public static string LocalBaseUrl = "https://192.168.0.102:20300";
        HttpClient client;




        /// <summary>
        ///     Object used to identify if the user tappep on a selected cell.
        /// </summary>
        private bool _isSelectedItemTap;

        /// <summary>
        ///     Object holding the reference of current user selection.
        /// </summary>
        private int _selectedItemIndex;

        private IList<CalibrationDetailRow> selectedItems = new List<CalibrationDetailRow>();

        private string calid;

        //public string CalId { get => calid; set => calid = value; }
        private string  idCallvar;

        public CalibrationOrderSetupPage(string calId)
        {
            InitializeComponent();
           
            BindingContext = this;
            idCallvar = calId;
            getDataBining();


        }

        private async void getDataBining()
        {
            multiSelectListItems = new ObservableCollection<CalibrationDetailRow>();

            var httpClientHandler = new HttpClientHandler();
           
            client = new HttpClient();
            workOrderModel = new WorkOrderModel();

            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/text"));

                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //specify to use TLS 1.2 as default connection
                // var id = "987654321ABCDEFG";
                if (!string.IsNullOrEmpty(idCallvar))
                {

                    var getResult = await client.GetAsync(LocalBaseUrl + "/api/entity/workorder/id/" + idCallvar);
                    if (getResult.IsSuccessStatusCode)
                    {

                        var response = await getResult.Content.ReadAsStringAsync();


                        var reqMonkeys = JsonConvert.DeserializeObject<WorkOrderModel>(response);
                        workOrderModel = reqMonkeys;
                        VinNumber.Text = workOrderModel.vinnumber;
                        WorkNumber.Text = workOrderModel.workorder;

                        MultiSelectListView.ItemsSource = workOrderModel.calibrationDetailRows;

                    }
                }
                else {
                    Debug.WriteLine("ID getting null ");
                }
               
            }
            catch (Exception ex)
            {
                
                Debug.WriteLine("Exception Error ", ex.ToString());
            }

                   

                    
        }

        private void MultiSelectListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void MultiSelectListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}