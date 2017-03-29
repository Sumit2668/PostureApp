using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading;
using Android.Util;
using Java.Util;
using System.Threading.Tasks;
using Xamarin.Forms;
using PostureApp.Droid.backgroundprocess;
using System.Threading;
using PostureApp.NativeMethod;
using Android.Widget;

[assembly: Dependency(typeof(SimpleStartedService))]
namespace PostureApp.Droid.backgroundprocess
{
    [Service]
    public class SimpleStartedService : Service//, IServiceCaller
    {
        static readonly string TAG = "X:" + typeof(SimpleStartedService).Name;
        static readonly int TimerWait = 4000;
        System.Threading.Timer timer;
        DateTime startTime;
        bool isStarted = false;

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public void startService()
        {
            Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(SimpleStartedService)));
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //Log.Debug(TAG, $"OnStartCommand called at {startTime}, flags={flags}, startid={startId}");
            if (isStarted)
            {
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                Toast.MakeText(this, "N", ToastLength.Short).Show();
            }
            else
            {
                startTime = DateTime.UtcNow;
                Toast.MakeText(this, "Y", ToastLength.Short).Show();
                timer = new System.Threading.Timer(HandleTimerCallback, startTime, 0, TimerWait);
                isStarted = true;
            }
            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            // This is a started service, not a bound service, so we just return null.
            return null;
        }


        public override void OnDestroy()
        {
            timer.Dispose();
            timer = null;
            isStarted = false;

            TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
            //Log.Debug(TAG, $"Simple Service destroyed at {DateTime.UtcNow} after running for {runtime:c}.");
            Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(SimpleStartedService)));
            base.OnDestroy();
        }

        void HandleTimerCallback(object state)
        {
            TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
            Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(SimpleStartedService)));
            // Toast.MakeText(this, "Log", ToastLength.Short).Show();
        }
    }

}