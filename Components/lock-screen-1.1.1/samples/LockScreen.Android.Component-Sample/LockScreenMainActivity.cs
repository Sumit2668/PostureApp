using System;

using Android.App;
using Android.Widget;
using Android.OS;

using LockScreen;

namespace LockScreen.Android.Sample
{
	[Activity (Label = "LockTest", MainLauncher = true)]
	public class LockScreenMainActivity : PasscodeProtectedActivity
//	public class LockScreenMainActivity : PasscodeProtectedActivity<CustomSettings>
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			FindViewById<Button> (Resource.Id.enableBtn).Click += EnableClicked;
			FindViewById<Button> (Resource.Id.disableBtn).Click += DisableClicked;
			FindViewById<Button> (Resource.Id.activateBtn).Click += ActivateClicked;
			FindViewById<Button> (Resource.Id.btnSecondActivity).Click += SecondActivityClicked;
		}

		void EnableClicked (object sender, EventArgs e)
		{
			Locker.Enable (this);
//			Locker<CustomSettings>.Enable(this);
		}

		void DisableClicked (object sender, EventArgs e)
		{
			Locker.Disable (this);
//			Locker<CustomSettings>.Disable(this);
		}
		
		void ActivateClicked (object sender, EventArgs e)
		{
			Locker.Activate (this);
//			Locker<CustomSettings>.Activate(this);
		}

		void SecondActivityClicked (object sender, EventArgs e)
		{
			StartActivity (typeof(SecondActivity));
		}
	
/*		If you don't want to use PasscodeProtectedActivity, you can use custom calls to Locker

		protected override void OnStart()
		{
			base.OnStart();
			Locker.OnStart(this);
		}

		protected override void OnPause()
		{
			base.OnPause();
			Locker.OnPause(this);
		}
*/
	}
}


