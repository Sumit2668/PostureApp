using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PostureApp.Droid.backgroundprocess;
using Xamarin.Forms;

[assembly: Dependency(typeof(snoozeservice))]
namespace PostureApp.Droid.backgroundprocess
{
    [Service]
    public  class snoozeservice : Service
    {
      
       
        static readonly string TAG = "X:" + typeof(SimpleStartedService).Name;
        static readonly int TimerWait = 1000 * 30;
        Timer timer;
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
            // Toast.MakeText(this, "Listener on snooze button", ToastLength.Long).Show();
            SimpleStartedService.snoozeClickEvent();
            return StartCommandResult.NotSticky;
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
            base.OnDestroy();
        }
     
    }
}