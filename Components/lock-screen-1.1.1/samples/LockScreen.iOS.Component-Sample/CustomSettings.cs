using System;

namespace LockScreen.iOS.Sample
{
	public class CustomSettings : LockScreen.DefaultSettings
	{
		public override void InitSettings ()
		{
			AutoSubmit = false;
			BackgroundView = new CustomBackground ();
			Messages = new CustomLockMessages ();
			PasscodeValidator = new CustomValidator ();
		}
	}
}

