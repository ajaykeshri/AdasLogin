
using ADASMobileClient.Droid;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyFrameRenderer), typeof(MyFrameRenderer))]
namespace ADASMobileClient.Droid
{
   public  class MyFrameRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(Android.Text.InputTypes.TextFlagNoSuggestions);
                Control.SetHintTextColor(Android.Content.Res.ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            }
        }
    }
}