using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADASMobileClient.Core.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
        }

        private void RadioButton_Clicked_OCR(object sender, EventArgs e)
        {

            DisplayAlert("Alert", "Scan by OCR", "OK");
        }

        private void RadioButton_Clicked_OR(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "Scan by QR", "OK");
        }
        private void RadioButton_Clicked_Barcode(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "Scan by Barcode", "OK");
        }

        private void RadioButton_Clicked_DirectInput(object sender, EventArgs e)
        {
            DisplayAlert("Alert", " Direct Input", "OK");
        }


        


    }
    
    
}
