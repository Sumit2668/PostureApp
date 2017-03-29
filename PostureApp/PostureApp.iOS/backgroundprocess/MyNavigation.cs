using System;
using Foundation;
using LocalNotifications.Plugin;
using LocalNotifications.Plugin.Abstractions;
using UIKit;
using Xamarin.Forms;

namespace PostureApp.iOS
{
	public class MyNavigation :UIViewController 
		//: global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate 
		                                           
	{
		public MyNavigation(IntPtr handle):base(handle)
		{
			
		}
		public MyNavigation()
		{ 
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var secondViewController = App.GetSecondPageIOS().CreateViewController();
			this.NavigationController.PresentViewController(secondViewController, true, null);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}



		public void MyNavigation1()
		{
			var secondViewController = App.GetSecondPageIOS().CreateViewController();
			this.NavigationController.PresentViewController(secondViewController, true, null);
			//NavigationController.PushViewController(secondViewController, true);
			//LoadApplication(new App("info"));
		}


}
}
