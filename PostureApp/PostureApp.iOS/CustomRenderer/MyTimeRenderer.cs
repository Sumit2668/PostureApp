using Foundation;
using PostureApp.CustomRenderer;
using PostureApp.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(MyTimePicker), typeof(MyTimeRenderer))]
namespace PostureApp.iOS.CustomRenderer
{
    public class MyTimeRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                Control.BackgroundColor = UIColor.Clear;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = UIColor.FromRGB(97, 187, 198);
                Control.Font = UIFont.FromName("s", 15);
            }
        }
    }
}