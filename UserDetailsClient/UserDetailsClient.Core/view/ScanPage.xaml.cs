using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using System.Threading.Tasks;
using ADASMobileClient.Core.model;
using Plugin.Media;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;

namespace ADASMobileClient.Core.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        string VinNumber;
        public Image imgTemplateImages;
        public Label lblImagePath;
        OcrModel ocrModel;
       
        StackLayout mainSTack = new StackLayout
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
        };

        public ScanPage()
        {
            InitializeComponent();

            
        
        }

        private  void RadioButton_Clicked_OCR(object sender, EventArgs e)
        {

            OcrMethodCall();
        }

        private async void OcrMethodCall()
        {
            ScanImage.SetValue(IsVisibleProperty, true);
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }
                else
                {
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        SaveToAlbum = true,
                    });

                    if (file == null)
                    {
                        return;
                    }

                    if (ScanImage != null)
                    {
                        //lblImagePath.Text = "ImagePath" + file.Path;
                        byte[] testByte = CommonFunctions.ReadStream(file.GetStream());
                        ////////////////////////////
                        var client = new HttpClient();
                        var queryString = HttpUtility.ParseQueryString(string.Empty);

                        // Request headers
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "de7609da413944308cde16098d1097f0");

                        var uri = "https://centralus.api.cognitive.microsoft.com/vision/v2.0/ocr" + queryString;

                        HttpResponseMessage response;

                        // Request body
                        // byte[] byteData = Encoding.UTF8.GetBytes("{testByte}");

                        using (var content = new ByteArrayContent(testByte))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            response = await client.PostAsync(uri, content);
                            var contents = response.Content.ReadAsStringAsync().Result;
                            Debug.WriteLine("OCR response " + contents);

                        }

                        var responseResult = await response.Content.ReadAsStringAsync();


                        ocrModel = JsonConvert.DeserializeObject<OcrModel>(responseResult);
                        var wordCount = Convert.ToString(ocrModel.regions[0].lines[0].words.Count);
                        StringBuilder numberList = new StringBuilder();
                        for (int i = 0; i < ocrModel.regions[0].lines[0].words.Count; i++)
                        {
                            numberList.Append(ocrModel.regions[0].lines[0].words[i].text);
                            numberList.Append(" ");
                        }
                        VinEnteredNumber.Text = numberList.ToString();

                        Debug.WriteLine("Car Numebr  " + numberList.ToString());

                        //////////////////////////////
                        ScanImage.Source = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            file.Dispose();
                            return stream;
                        });
                        //  mainSTack.IsVisible = false;
                    }
                }
            }
            catch (Exception ex) {
                await DisplayAlert("Alert", " " + ex, "OK");
            }
        }

        private async void RadioButton_Clicked_OR(object sender, EventArgs e)
        {
            await NewMethod();
        }
        private async void RadioButton_Clicked_Barcode(object sender, EventArgs e)
        {
            // await DisplayAlert("Alert", "Scan by Barcode", "OK");

            await NewMethod();

        }

        private async Task NewMethod()
        {
            ScanImage.SetValue(IsVisibleProperty, true);

            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }
                else
                {
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        SaveToAlbum = true,
                    });

                    if (file == null)
                    {
                        return;
                    }

                    if (ScanImage != null)
                    {

                        ScanImage.Source = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            file.Dispose();
                            return stream;
                        });
                        //  mainSTack.IsVisible = false;
                    }

                }

                var scan = new ZXingScannerPage();
                await Navigation.PushAsync(scan);
                scan.OnScanResult += (result) =>
                {
                    ZXing.BarcodeFormat barcodeFormat = result.BarcodeFormat;
                    string type = barcodeFormat.ToString();
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();

                        VinEnteredNumber.Text = result.Text;
                    });

                };
            }catch(Exception ex)
            {
                await DisplayAlert("Alert", " " +ex , "OK");

            }
        }

        private void RadioButton_Clicked_DirectInput(object sender, EventArgs e)
        {
            ScanImage.SetValue(IsVisibleProperty, false); // the view is GONE, not invisible
            VinEnteredNumber.Text=null;
              VinNumber  = VinEnteredNumber.Text;

            
           

        }

        private void Confirm_Button_Clicked(object sender, EventArgs e)
        {
            
            new NavigationPage(new Calibration(VinNumber));

        }



       

    }


}
