using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using PostureApp.iOS.backgroundprocess;
using UIKit;
using Xamarin.Forms;

namespace PostureApp.iOS
{
	public class Application:UIViewController
    {
        // This is the main entry point of the application.
		public static void Main(string[] args)
        {
			UIApplication.Main(args, null, "AppDelegate");
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here
			//if (args.Count() == 0)
			//{
			//	UIApplication.Main(args, null, "AppDelegate");
			//}
			//else
			//{
			//	var del = (AppDelegate)UIApplication.SharedApplication.Delegate;// (args, null, "AppDelegate");
			//	//UIApplication.Main(args, null, "OkayDelegate");
			//}
		}
    }
}

