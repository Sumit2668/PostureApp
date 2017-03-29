using System;
using System.Drawing;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

using LockScreen;

namespace LockScreen.iOS.Sample
{
	public partial class LockTestViewController : UIViewController
	{
		public LockTestViewController () : base ("LockTestViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		partial void activateClicked (NSObject sender)
		{
			Locker.Activate (this);
//			Locker<CustomSettings>.Activate(this);
		}

		partial void enableClicked (NSObject sender)
		{
			Locker.Enable (this);
//			Locker<CustomSettings>.Enable(this);
		}

		partial void disableClicked (NSObject sender)
		{
			Locker.Disable ();
//			Locker<CustomSettings>.Disable();
		}
	}
}

