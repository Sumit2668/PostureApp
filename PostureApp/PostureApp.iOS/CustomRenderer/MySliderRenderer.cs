using PostureApp.CustomRenderer;
using PostureApp.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MySlider), typeof(MySliderRenderer))]
namespace PostureApp.iOS.CustomRenderer
{
    public class MySliderRenderer:SliderRenderer
    {
        //UISlider sliderImage;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Slider> e)
        {
            base.OnElementChanged(e);
           
            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                var slider = (UISlider)Control;
                slider.ThumbTintColor = UIColor.White;
                slider.MinimumTrackTintColor = UIColor.White;
                slider.MaximumTrackTintColor = UIColor.Black;
                //sliderImage.SetThumbImage(UIImage.FromFile("29_icon.png"), UIControlState.Normal);
               
            }
        }
    }
}