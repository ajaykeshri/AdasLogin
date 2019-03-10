using ADASMobileClient.Core.model;
using ADASMobileClient.Core.ViewModel;
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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ADASMobileClient.Core
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GrideListPage : ContentPage
	{
        public List<string> People { get; set; }
        HttpClient client;
        private List<WorkOrderModel>  workOrderItems ;
     
    

        public GrideListPage()
        {
       
            InitializeComponent();
            BindingContext = new WorkOrderViewModel();

          
        }

     

        private async void CalibratinButon_Clicked(object sender, EventArgs e)
        {

           // var cal = new CalibrationOrderSetupPage();
            var CalId = "1FMZU35POXZA54953";
           

            await Navigation.PushAsync( new CalibrationOrderSetupPage(CalId));
        }
        private async void NewCalibratinButon_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CalibrationOrderSetup());
            //  Application.Current.MainPage = new NavigationPage(new OrderPage());
            await Navigation.PushAsync(new Calibration());
        }

        
    }
    public class TextChangedBehavior : Behavior<SearchBar>
    {
        protected override void OnAttachedTo(SearchBar bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);
        }
    }
}