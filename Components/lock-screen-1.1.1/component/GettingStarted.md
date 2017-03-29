`LockScreen` adds passcode protection to your iOS or Android apps in a few lines
of code. It supports easy customization of background images, passcode validation
mechanisms, and string localization.

## Examples

### Adding `LockScreen` to your iOS app:

In order to actually prompt for a passcode, add the following lines to
your `AppDelegate`:

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

The `Activate` method checks if a passcode was set, and if it was,
presents the lock screen.

To ask a user to set a passcode (usually called from you app's settings
screen):

```csharp
using LockScreen;
...

public override void ViewDidAppear (bool animated)
{
  base.ViewDidAppear (animated);

  Locker.Enable (this);

  // Conversely,
  Locker.Disable ();
}
```

### Adding `LockScreen` to your Android app:

Create a passcode-protected `Activity` by subclassing `PasscodeProtectedActivity`:

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

Enabling or disabling the lock screen requires a `Context`:

```csharp
using LockScreen;
...

Locker.Enable (context);
Locker.Disable (context);
```  

### Customization and Localization

To customize the lock screen, first create a subclass of
`DefaultSettings`:

```csharp
using LockScreen;
...

public class MyLockSettings : DefaultSettings
{
  public override void InitSettings ()
  {
    // Initialize default settings
    base.InitSettings();

    // Localized lock messages
    Messages = new MyLockMessages ();
    // Custom passcode validation
    PasscodeValidator = new MyLockValidator ();

    // Change other default settings
    AutoSubmit = false;
    BackgroundView = new MyLockBackground ();
  }
}

// Tip: define a trivial subclass to make calling static
// Locker<T> methods more succinct:
class MyLocker : Locker<MyLockerSettings> { }
``` 

You may want to use a custom passcode validator if you want to save
passcodes into your own database or service for example.
`DefaultPasscodeValidator` uses `NSUserDefaults` to save passcode, so
passcodes are safely backed up to iCloud by default.
