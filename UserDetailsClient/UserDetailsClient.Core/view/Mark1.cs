using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Plugin.Media;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Text;
using ADASMobileClient.Core.model;
using Newtonsoft.Json;

namespace ADASMobileClient.Core.view
{
    class Mark1 : ContentPage
    {
        public Image imgTemplateImages;
        public Label lblImagePath;
        OcrModel ocrModel;
        string carNumber;
        public Mark1()
        {
            imgTemplateImages = new Image();
            Button btnAddPic = new Button
            {
                Text = "Click Pic",
            };
            lblImagePath = new Label
            {
                TextColor = Color.Red,
            };
            StackLayout mainSTack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            btnAddPic.Clicked += AddPic_Clicked;
            mainSTack.Children.Add(imgTemplateImages);
            mainSTack.Children.Add(btnAddPic);
            mainSTack.Children.Add(lblImagePath);
            Content = mainSTack;
        }

        void AddPic_Clicked(object sender, EventArgs e)
        {
           var path= DisplayImageOption();
           //  DisplayAlert("File Path", ""+ path, "OK");

        }
        async Task DisplayImageOption()
        {
            var option = await DisplayActionSheet("Add Image", "Cancel", null, "Take a photo");
            if (option != null)
            {
                string Filename = "";
                Filename = Guid.NewGuid().ToString();
                Filename = Filename.Substring(0, 6);
                switch (option.ToString())
                {
                    case "Take a photo":

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

                                if (imgTemplateImages != null)
                                {
                                    lblImagePath.Text = "ImagePath" + file.Path;
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
                                        numberList.Append("");
                                    }
                                    await DisplayAlert("Alert", "Car Numebr :" + numberList.ToString(), "Ok");
                                    Debug.WriteLine("Car Numebr  " + numberList.ToString());

                                    //////////////////////////////
                                    imgTemplateImages.Source = ImageSource.FromStream(() =>
                                    {
                                        var stream = file.GetStream();
                                        file.Dispose();
                                        return stream;
                                    });
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            await DisplayAlert("Alert", " "+ ex.StackTrace, "Ok");
                        }
                        break;

                    default:
                        await DisplayAlert("Alert", "Something Went Wrong", "Ok");
                        break;
                }
            }
        }

        
    }
}