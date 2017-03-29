using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using LockScreen;

namespace LockScreen.Android.Sample
{
	[Activity (Label = "SecondActivity")]			
	public class SecondActivity : PasscodeProtectedActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.SecondLayout);

			FindViewById<Button> (Resource.Id.button1).Click += (sender, e) => {
				StartActivity (typeof(LockScreenMainActivity));
			};
		}
	}
}

