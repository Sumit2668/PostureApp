using System;
using AppPostureApp.CustomRenderer;
using PostureApp.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyFrame), typeof(MyFrameRenderer))]
namespace PostureApp.iOS
{
	public class MyFrameRenderer : FrameRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			Layer.CornerRadius = 10;
		}
	}
}

