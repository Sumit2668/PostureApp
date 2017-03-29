using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		LockTestViewController viewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new LockTestViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			Locker.Activate (window);
			return true;
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// It's put here intentionally to allow lock screen be opened immediately on app restore.
			// Otherwise you will first see app UI and then Locker will flash.
			Locker.Activate (window);
		}
	}


}
