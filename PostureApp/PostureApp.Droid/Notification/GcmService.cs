using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Android.Util;
using Gcm.Client;
//using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;



[assembly: Permission(Name = "com.Posture.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.Posture.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]

namespace PostureApp.Droid.Notification
{

    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "com.Posture" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "com.Posture" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "com.Posture" })]

    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = new string[] { "225191918224" };
    }


    [Service]
    public class GcmService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        // This is any integer value unique to the application.
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        public GcmService()
            : base(PushHandlerBroadcastReceiver.SENDER_IDS) { }
        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);
            RegistrationID = registrationId;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //var notification = new Notification.Builder(this).Build();
            //StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            return base.OnStartCommand(intent, flags, startId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            if (intent != null && intent.Extras != null)
            {
                createNotification(intent.Extras.Get("google.c.a.c_l").ToString(), intent.Extras.Get("gcm.notification.body").ToString());
            }
        }
        void createNotification(string desc, string title)
        {
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            var uiIntent = new Intent(this, typeof(MainActivity));

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                    .SetSmallIcon(Android.Resource.Drawable.IcLockIdleAlarm)
                    .SetTicker(title)
                    .SetContentTitle(title)
                    .SetContentText(desc)
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                    .SetAutoCancel(true).Build();

            notificationManager.Notify(1, notification);

        }
        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "Unregistered RegisterationId : " + registrationId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);
        }
    }
}
