`LockScreen` adds passcode protection to your iOS or Android apps in a few lines
of code. It supports easy customization of background images, passcode validation
mechanisms, and string localization.

### Adding `LockScreen` to your iOS app:

```csharp
using LockScreen;
...

public partial class AppDelegate : UIApplicationDelegate
{
  UIWindow window;
  ...

  public override bool FinishedLaunching (UIApplication app, NSDictionary options)
  {
    ...
    Locker.Activate (window);
    return true;
  }

  public override void DidEnterBackground (UIApplication application)
  {
    ...
    Locker.Activate (window);
  }
}
```  

### Adding `LockScreen` to your Android app:

To add a `LockScreen` to your Android app, you can create a
passcode-protected `Activity` by subclassing `PasscodeProtectedActivity`:

```csharp
using LockScreen;
...

public class MyProtectedActivity : PasscodeProtectedActivity
{
  ...
}
```  

Rather than subclassing `PasscodeProtectedActivity`, you may also call
`Locker.OnStart` and `Locker.OnPause` in your `OnStart` and `OnPause`
methods for each activity in your app.

A final option is to manually call `Locker.Activate` whenever you'd like
to prompt for a passcode.

*Screenshot assembled with [PlaceIt](http://placeit.breezi.com).*
