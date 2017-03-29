using System;
using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using PostureApp.CustomRenderer;
using PostureApp.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyTimePicker), typeof(MyTimeRenderer))]
namespace PostureApp.Droid.CustomRenderer
{
   public class MyTimeRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
               // Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.SetTextColor(global::Android.Graphics.Color.Rgb(97, 187, 198));
                Control.TextSize = 14;
            }
        }
    }
}