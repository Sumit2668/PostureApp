using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;


namespace PostureApp.Droid.backgroundprocess
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new string[] { Intent.ActionBootCompleted }, Priority = Int32.MaxValue)]
    public  class alarmreceiver :  WakefulBroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //  Toast.MakeText(Android.App.Application.Context, "Receiver:OnReceive", ToastLength.Long).Show();
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            var contentIntent = PendingIntent.GetActivity(Android.App.Application.Context, 0, new Intent(Android.App.Application.Context, typeof(okactivity)), 0);
            var okPIntent = PendingIntent.GetActivity(Android.App.Application.Context, 0, new Intent(Android.App.Application.Context, typeof(okactivity)), 0);
            var snoozePIntent = PendingIntent.GetService(Android.App.Application.Context, 0, new Intent(Android.App.Application.Context, typeof(snoozeservice)), 0);
            var manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);

            //Generate a notification with just short text and small icon
            var builder = new NotificationCompat.Builder(Android.App.Application.Context)
	                        .SetContentIntent(contentIntent)
	                        .SetSmallIcon(Resource.Drawable.icon)
	                        .SetContentTitle(title)
	                        .SetContentText(message)
	                        .SetStyle(style)
	                        .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
	                        .SetAutoCancel(true)
	                        .AddAction(Resource.Drawable.icon, "Ok", okPIntent)
	                        .AddAction(Resource.Drawable.icon, "Snooze", snoozePIntent);
           
            var notification = builder.Build();
            manager.Notify(0, notification);
        }

        public void SetNotification(DateTime dateTime,string title, string message)
        {
            var alarmMgr = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            var intent = new Intent(Android.App.Application.Context, typeof(alarmreceiver));
            intent.PutExtra("title", title);
            intent.PutExtra("message", message);
            
            StartWakefulService(Android.App.Application.Context, intent);
            CompleteWakefulIntent(intent);

            PendingIntent alarmIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, 0);

            var millsTillTriger = (long)dateTime.TimeOfDay.Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds;
            alarmMgr.Set(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + millsTillTriger,
                     alarmIntent);
        }      
    }


    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new string[] {Intent.ActionBootCompleted}, Priority = Int32.MaxValue)]
    public class Exceptionalarmreceiver : WakefulBroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //  Toast.MakeText(Android.App.Application.Context, "Receiver:OnReceive", ToastLength.Long).Show();
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");
            var contentIntent = PendingIntent.GetActivity(Android.App.Application.Context, 0, new Intent(Android.App.Application.Context, typeof(MainActivity)), 0);
            var manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);

            //Generate a notification with just short text and small icon
           var builder = new NotificationCompat.Builder(Android.App.Application.Context)
		                .SetContentIntent(contentIntent)
		                .SetSmallIcon(Resource.Drawable.icon)
		                .SetContentTitle(title)
		                .SetContentText(message)
		                .SetStyle(style)
		                .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
		                .SetAutoCancel(true);
		                       

            var notification = builder.Build();
            manager.Notify(0, notification);
        }

        public void SetNotificationForException(DateTime dateTime, string title, string message)
        {
            var alarmMgr = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            var intent = new Intent(Android.App.Application.Context, typeof(Exceptionalarmreceiver));
            intent.PutExtra("title", title);
            intent.PutExtra("message", message);

            StartWakefulService(Android.App.Application.Context, intent);
            CompleteWakefulIntent(intent);

            //NofiticationReceiver.CompleteWakefulIntent(intent);
            PendingIntent alarmIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, 0);

            var millsTillTriger = (long)dateTime.TimeOfDay.Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds;

            alarmMgr.Set(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + millsTillTriger,
                     alarmIntent);
        }
    }

}