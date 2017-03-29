

using Android.Graphics;
using Android.Widget;
using PostureApp.CustomRenderer;
using PostureApp.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MySlider), typeof(MySliderRenderer))]

namespace PostureApp.Droid.CustomRenderer
{
    public class MySliderRenderer:SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Slider> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var seekBar = (SeekBar)Control;
                seekBar.ProgressDrawable.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.White.ToAndroid(), PorterDuff.Mode.SrcIn));
                seekBar.Thumb.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.White.ToAndroid(), PorterDuff.Mode.SrcIn));
                //seekBar.ProgressTintList = Android.Content.Res.ColorStateList.ValueOf(Xamarin.Forms.Color.Black.ToAndroid());
                //seekBar.IndeterminateDrawable.SetColorFilter(Xamarin.Forms.Color.Blue.ToAndroid(), PorterDuff.Mode.SrcIn);
                
            }
        }
       
    }
}