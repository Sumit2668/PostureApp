using System;

namespace LockScreen.Android.Sample
{
	public class CustomLockMessages : LockScreen.DefaultMessages
	{
		public CustomLockMessages ()
		{
			this.EnterPasscode = "Wazzzzup?";
			this.ConfirmPasscode = "You'll never repeat it again!";
			this.TryAgain = "ORLY???";
		}
	}
}

