using System;

using Android.Content;

namespace LockScreen.Android.Sample
{
	public class CustomSettings : LockScreen.DefaultSettings
	{
		public override void InitSettings (Context context)
		{
			AutoSubmit = false;
			Messages = new CustomLockMessages ();
			PasscodeValidator = new CustomValidator ();
			BackgroundView = new CustomBackground(context);
		}
	}
}

