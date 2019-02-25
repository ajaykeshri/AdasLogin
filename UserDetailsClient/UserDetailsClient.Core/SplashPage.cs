using Xamarin.Forms;

namespace UserDetailsClient.Core
{
    internal class SplashPage : ContentPage
    {
        Image splashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "splash_car.png",
                HeightRequest = 1000,
                WidthRequest = 1000


            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashImage);
            this.BackgroundColor = Color.FromHex("#429de3");
            this.Content = sub;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(0.9, 500, Easing.Linear);
            await splashImage.ScaleTo(1.9, 500, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new MainPage());

        }

    }
}