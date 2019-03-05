using System;
using System.Collections.Generic;
using System.Linq;
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

        public GrideListPage()
        {
            InitializeComponent();
            People = new List<string>
            {
                "Alan",
                "Betty",
                "Charles",
                "David",
                "Edward",
                "Francis",
                "Gary",
                "Helen",
                "Ivan",
                "Joel",
                "Kelly",
                "Larry",
                "Mary",
                "Nancy",
                "Olivia",
                "Peter",
                "Quincy",
                "Robert",
                "Stephen",
                "Timothy",
                "Ursula",
                "Vincent",
                "William",
                "Xavier",
                "Yvonne",
                "Zack"
            };
            CV.BindingContext = this;
        }

        private async void CalibratinButon_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CalibrationOrderSetup());
            //  Application.Current.MainPage = new NavigationPage(new OrderPage());
            await Navigation.PushAsync(new CalibrationOrderSetupPage());
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