using System;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace LockScreen.iOS.Sample
{
	public class CustomBackground : UIView
	{
		public CustomBackground ()
		{
			BackgroundColor = UIColor.Purple;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
		}
	}
}