using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace PostureApp.iOS
{
   
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		public override UIWindow Window
		{
			get;
			set;
		}
		 private MyNavigation viewController;
		 private UIWindow window;
        public override bool FinishedLaunching(UIApplication app, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

	          if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
				{
					var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
						UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
				);
				app.RegisterUserNotificationSettings(notificationSettings);
				}

			// check for a notification
			if (launchOptions != null)
			{
				// check for a local notification
				if (launchOptions.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
				{
					var localNotification = launchOptions[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if (localNotification != null)
					{
						UIAlertController okayAlertController = UIAlertController.Create(localNotification.AlertAction, localNotification.AlertBody, UIAlertControllerStyle.Alert);
						okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

						Window.RootViewController.PresentViewController(okayAlertController, true, null);

						// reset our badge
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					}
				}
			}

				return base.FinishedLaunching(app, launchOptions);
			//return true;
        }

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{

			 UIAlertView alert = new UIAlertView() { Title = notification.AlertAction, Message = notification.AlertBody };
			 alert.AddButton("OK");
			 alert.AddButton("Cancel");
			 alert.Clicked += (object s, UIButtonEventArgs ev) =>
				{
					if (ev.ButtonIndex == 0)
					{
						//string[] arg = new string[1];
						//arg[0] = "ok delegate";
						//PostureApp.iOS.Application.Main(arg);

						//MyNavigation nav = new MyNavigation();
						//nav.MyNavigation1();
						viewController = new MyNavigation();
						window = new UIWindow(UIScreen.MainScreen.Bounds);
						window.RootViewController = viewController;
						window.MakeKeyAndVisible();

					}
					else
					{
						//player.Pause();
						//player.Stop();
						//var manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
						//manager.Cancel(0);
						//snoozeTime = snoozeTimeCurrentSelected;
					}
				};
			 alert.Show();		

			//// show an alert
			//UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
			//okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			//Window.RootViewController.PresentViewController(okayAlertController, true, null);
			//// reset our badge
			//UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

			 // show an alert
   //         UIAlertController okayAlertController = UIAlertController.Create (notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
   //         okayAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
   //         viewController.PresentViewController (okayAlertController, true, null);
			//// reset our badge
          UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

			public override void OnResignActivation(UIApplication application)
			{
				// Invoked when the application is about to move from active to inactive state.
				// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
				// or when the user quits the application and it begins the transition to the background state.
				// Games should use this method to pause the game.
			}

			public override void DidEnterBackground(UIApplication application)
			{
				// Use this method to release shared resources, save user data, invalidate timers and store the application state.
				// If your application supports background exection this method is called instead of WillTerminate when the user quits.
			}

			public override void WillEnterForeground(UIApplication application)
			{
				// Called as part of the transiton from background to active state.
				// Here you can undo many of the changes made on entering the background.
			}

			public override void OnActivated(UIApplication application)
			{
				// Restart any tasks that were paused (or not yet started) while the application was inactive. 
				// If the application was previously in the background, optionally refresh the user interface.
			}

			public override void WillTerminate(UIApplication application)
			{
				// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
			}

		// public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		// {
		//	UIViewController viewController = new UIViewController();
		//	//show an alert		

		//	//UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
		//	//okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
		//	//Window.RootViewController.PresentViewController(okayAlertController, true, null);
		//	//UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

		//	// show an alert
		//          UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
		//	okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
		//    viewController.PresentViewController(okayAlertController, true, null);

		//	// reset our badge
		//	UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		// }


	}

	// The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
 //   [Register("AppDelegate")]
	//public class AppDelegate : UIApplicationDelegate
	//{
	//	// class-level declarations

	//	public override UIWindow Window
	//	{
	//		get;
	//		set;
	//	}


	//	public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
	//	{
	//		if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
	//		{
	//			var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
	//				UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
	//			);
	//			application.RegisterUserNotificationSettings(notificationSettings);
	//		}

	//		// check for a notification
	//		if (launchOptions != null)
	//		{
	//			// check for a local notification
	//			if (launchOptions.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
	//			{
	//				var localNotification = launchOptions[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
	//				if (localNotification != null)
	//				{
	//					UIAlertController okayAlertController = UIAlertController.Create(localNotification.AlertAction, localNotification.AlertBody, UIAlertControllerStyle.Alert);
	//					okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

	//					Window.RootViewController.PresentViewController(okayAlertController, true, null);

	//					// reset our badge
	//					UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
	//				}
	//			}
	//		}
	//		return true;
	//	}

	//	public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
	//	{
	//		// show an alert
	//		UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
	//		okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

	//		Window.RootViewController.PresentViewController(okayAlertController, true, null);

	//		// reset our badge
	//		UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
	//	}

	//	public override void OnResignActivation(UIApplication application)
	//	{
	//		// Invoked when the application is about to move from active to inactive state.
	//		// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
	//		// or when the user quits the application and it begins the transition to the background state.
	//		// Games should use this method to pause the game.
	//	}

	//	public override void DidEnterBackground(UIApplication application)
	//	{
	//		// Use this method to release shared resources, save user data, invalidate timers and store the application state.
	//		// If your application supports background exection this method is called instead of WillTerminate when the user quits.
	//	}

	//	public override void WillEnterForeground(UIApplication application)
	//	{
	//		// Called as part of the transiton from background to active state.
	//		// Here you can undo many of the changes made on entering the background.
	//	}

	//	public override void OnActivated(UIApplication application)
	//	{
	//		// Restart any tasks that were paused (or not yet started) while the application was inactive. 
	//		// If the application was previously in the background, optionally refresh the user interface.
	//	}

	//	public override void WillTerminate(UIApplication application)
	//	{
	//		// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
	//	}
	//}

}
