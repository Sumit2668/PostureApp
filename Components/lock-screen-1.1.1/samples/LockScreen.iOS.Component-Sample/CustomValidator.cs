using System;

using LockScreen;

namespace LockScreen.iOS.Sample
{
	public class CustomValidator : IPasscodeValidator
	{

		public bool HasPasscode ()
		{
			return true;
		}

		public void SetPasscode (string passcode)
		{
			// Save passcode here
		}

		public void RemovePasscode ()
		{
			// Unset passcode here
		}

		public bool ValidatePasscode (string passcode)
		{
			return passcode == "0000";
		}

	}
}

