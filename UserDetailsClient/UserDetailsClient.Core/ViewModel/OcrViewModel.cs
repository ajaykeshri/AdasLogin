using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

using System.Threading.Tasks;

using Xamarin.Forms;

using Plugin.Media;
using Plugin.Media.Abstractions;
using System.ComponentModel;
using System.Diagnostics;

using static System.Diagnostics.Debug;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ADASMobileClient.Core.ViewModel
{
    class OcrViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Invoice> Invoices { get; } = new ObservableCollection<Invoice>();

        public string Message { get; set; } = "Hello World!";

        Command addInvoiceCommand = null;
        public Command AddInvoiceCommand =>
                    addInvoiceCommand ?? (addInvoiceCommand = new Command(async () => await ExecuteAddInvoiceCommandAsync()));


        async Task ExecuteAddInvoiceCommandAsync()
        {




            double total = 0.0;
            try
            {
                IsBusy = true;
                // 1. Add camera logic.
                await CrossMedia.Current.Initialize();

                MediaFile photo;
                if (CrossMedia.Current.IsCameraAvailable)
                {
                    photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {

                     
                        Directory = "Sample",
                        Name = "test.jpg"
                    });
                }
                else
                {
                    photo = await CrossMedia.Current.PickPhotoAsync();
                }

                if (photo == null)
                {
                    PrintStatus("Photo was null :(");
                    return;
                }



                await MakeRequestAsync();


                //// 2. Add  OCR logic.
                //OcrResults text;

                //var client = new VisionServiceClient("15e5da6a012448ef86fa94dc7e20480b");

                //using (var stream = photo.GetStream())
                //    text = await client.RecognizeTextAsync(stream);

                //var numbers = from region in text.Regions
                //              from line in region.Lines
                //              from word in line.Words
                //              where word?.Text?.Contains("$") ?? false
                //              select word.Text.Replace("$", string.Empty);


                //double temp = 0.0;
                //total = numbers?.Count() > 0 ?
                //        numbers.Max(x => double.TryParse(x, out temp) ? temp : 0) :
                //        0;



                //PrintStatus($"Found total {total.ToString("C")} " +
                //    $"and we had {text.Regions.Count()} regions");


                // 3. Add to data-bound collection.
                Invoices.Add(new Invoice
                {
                    Total = total,
                    Photo = photo.Path,
                    TimeStamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                await (Application.Current?.MainPage?.DisplayAlert("Error",
                    $"Something bad happened: {ex.Message}", "OK") ??
                    Task.FromResult(true));

                PrintStatus(string.Format("ERROR: {0}", ex.Message));

            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task MakeRequestAsync()
        {
            var client = new HttpClient();
            var queryString = System.Net.WebUtility.UrlEncode(string.Empty);
            //var queryString = HttpUtility.ParseQueryString(string.Empty);


            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "de7609da413944308cde16098d1097f0");


            // Request parameters
            //queryString["language"] = "unk";
            //  queryString["detectOrientation"] = "true";
            var uri = "https://centralus.api.cognitive.microsoft.com/vision/v2.0/ocr";

            HttpResponseMessage response;

            Dictionary<string, string> sample2 = new Dictionary<string, string>();
            sample2.Add("url", "https://dummyimage.com/600x400/000/fff&text=Dl-7S2514");


            //// Request body
            //byte[] byteData = Encoding.UTF8.GetBytes(imageUrl);



            //using (var content = new ByteArrayContent(byteData))
            //{
            //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //    response = await client.PostAsync(uri, content);
            //    Debug.WriteLine("OCR response "+response);

            //}

            var content = new StringContent(JsonConvert.SerializeObject(sample2), Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);
            var contents = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("OCR response " + contents);



        }

        public void PrintStatus(string helloWorld)
        {
            if (helloWorld == null)
                throw new ArgumentNullException(nameof(helloWorld));

            WriteLine(helloWorld);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                if (busy == value)
                    return;

                busy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Message));
            }
        }


    }
}