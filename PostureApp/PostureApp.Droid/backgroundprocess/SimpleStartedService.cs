using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using System.Threading;
using PostureApp.NativeMethod;
using PostureApp.Droid.backgroundprocess;
using Xamarin.Forms;
using PostureApp.Model;
using Android.Media;
using System.Threading.Tasks;
using PostureApp.StaticModels;
using Android.Support.V7.App;

[assembly: Dependency(typeof(SimpleStartedService))]
namespace PostureApp.Droid.backgroundprocess
{
    [Service]
    public class SimpleStartedService : Service, IServiceCaller
    {
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        public string title = "Reminder";
        public string message = "Your exercise is ready click on OK to continue!";


        public static ScheduleTbl scheduleTbl = StaticScheduleMdl.scheduleTbl;//new ScheduleTbl();
        public ScheduleMdl scheduleMdl = new ScheduleMdl();
        public static List<MovementListTbl> movementListTbl = StaticExcerciseMdl.movementListTbl; //new List<MovementListTbl>();
        public static List<MovementListMdl> movementListMdl = new List<MovementListMdl>();
        public static List<MovementListMdl> movementListMdl_temp = new List<MovementListMdl>();
        public static MovementListMdl currentSelectedExercise = new MovementListMdl();
        public static MediaPlayer player;
        static readonly string TAG = "X:" + typeof(SimpleStartedService).Name;
        static readonly int TimerWait = 1000 * 60 * 1;
        private static int snoozeTime = 0;
        private readonly static int snoozeTimeCurrentSelected = 5;


        Timer timer;
        DateTime startTime;
        bool isStarted = false;

        private int tempCount = 0;
        private int Count = 0;

        readonly string Monday = "Monday";
        readonly string Tuesday = "Tuesday";
        readonly string Wednesday = "Wednesday";
        readonly string Thursday = "Thursday";
        readonly string Friday = "Friday";
        readonly string Saturday = "Saturday";
        readonly string Sunday = "Sunday";


        private readonly string Notification = "Default Notification";
        private readonly string Alarm = "Default Alarm";
        private readonly string Ringtone = "Default Ringtone";

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
            try
            {
                if (isStarted)
                {
                    if (tempCount < Count)
                    {
                        tempCount = Count = 0;
                        prepairNotification();
                        TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                    }
                }
                else
                {
                    startTime = DateTime.UtcNow;
                    timer = new Timer(HandleTimerCallback, startTime, 0, TimerWait);
                    isStarted = true;

                    var contentIntent = PendingIntent.GetActivity(Android.App.Application.Context, 0, new Intent(Android.App.Application.Context, typeof(MainActivity)), 0);
                    var builder = new NotificationCompat.Builder(Android.App.Application.Context)
                       .SetContentIntent(contentIntent)
                       .SetSmallIcon(Resource.Drawable.ic_launcher)
                       .SetContentTitle("PostureApp")
                       //.SetContentTitle(Resources.GetString(Resource.String.ApplicationName))
                       .SetContentText("Service is running..")
                       .SetOngoing(true)
                       .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis());
                    var notification = builder.Build();
                    StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
                }
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "OnStartCommand");
            }
            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            // This is a started service, not a bound service, so we just return null.
            return null;
        }

        private async Task<int> fillExercise()
        {
            try
            {
                movementListTbl = StaticExcerciseMdl.movementListTbl;
                if (movementListTbl == null)
                {
                    movementListTbl = new List<MovementListTbl>();
                }

                if (movementListMdl.Count == 0)
                {

                    //  movementListTbl = await App.mvDatabase.GetItemsAsync().ConfigureAwait(false);
                    foreach (var item in movementListTbl.Where(ml => ml.Selected == true))
                    {
                        movementListMdl.Add(new MovementListMdl(item));
                    }
                }
                else
                {
                    //movementListTbl = await App.mvDatabase.GetItemsAsync().ConfigureAwait(false);
                    movementListMdl_temp = new List<MovementListMdl>();
                    foreach (var item in movementListTbl.Where(m2 => m2.Selected == true))
                    {
                        movementListMdl_temp.Add(new MovementListMdl(item));
                    }
                    var itemsToRemove = new List<MovementListMdl>();
                    foreach (var movement in movementListMdl)
                    {
                        var temp_movement = movementListMdl_temp.FirstOrDefault(x => x.Id == movement.Id);
                        if (temp_movement == null)
                        {
                            itemsToRemove.Add(movement);
                        }
                    }
                    foreach (var removeitem in itemsToRemove)
                    {
                        movementListMdl.Remove(removeitem);
                    }

                }

            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "fillExercise");
            }

            return 1;
        }

        private int fillSchedule()
        {
            int mycount = 0;
            scheduleTbl = StaticScheduleMdl.scheduleTbl;
            try
            {
                mycount = 1;
                //scheduleTbl = SchedulePCL.GetScheduleTbl().Result;
                if (scheduleTbl == null)
                {
                    mycount = 3;
                    scheduleTbl = new ScheduleTbl();
                }
                mycount = 4;
                scheduleMdl = ScheduleMdl.PrepareList(scheduleTbl);
                mycount = 5;
                scheduleMdl.MoveFreq_Int = scheduleMdl.GetFrequency();
                mycount = 6;
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "fillSchedule " + mycount);
            }
            return 1;
        }

        private async void prepairNotification()
        {
            try
            {
                fillSchedule();
                await fillExercise();

                currentSelectedExercise = movementListMdl.FirstOrDefault();
                if (currentSelectedExercise != null)
                {
                    TimeSpan DayStarts = new TimeSpan(scheduleMdl.DayStarts.Ticks);
                    TimeSpan DayEnds = new TimeSpan(scheduleMdl.DayEnds.Ticks);
                    TimeSpan currenttime = DateTime.Now.TimeOfDay;
                    string currentDay = DateTime.Now.ToString("dddd");

                    if (DayStarts <= currenttime && DayEnds > currenttime)
                    {
                        if ((currentDay == Sunday && scheduleMdl.Sun == true) || (currentDay == Monday && scheduleMdl.Mon == true) || (currentDay == Tuesday && scheduleMdl.Tue == true) || (currentDay == Wednesday && scheduleMdl.Wed == true) || (currentDay == Thursday && scheduleMdl.Thu == true) || (currentDay == Friday && scheduleMdl.Fri == true) || (currentDay == Saturday && scheduleMdl.Sat == true))
                        {
                            int reminder = (int)(currenttime.TotalMinutes % scheduleMdl.MoveFreq_Int);
                            // reminder = 0;
                            if (reminder == 0 || currenttime == DayStarts)
                            {
                                if (snoozeTime > 0)//skip current selected exercise because it's time is over
                                {
                                    SimpleStartedService.movementListMdl.Remove(SimpleStartedService.currentSelectedExercise);
                                    await fillExercise();
                                    currentSelectedExercise = movementListMdl.FirstOrDefault();
                                }

                                if (currentSelectedExercise != null)
                                {
                                    prepairMessageForNotification();
                                }
                            }
                            else
                            {
                                if (snoozeTime > 0)
                                {
                                    reminder = (int)(currenttime.TotalMinutes % snoozeTime);
                                    if (reminder == 0 || currenttime == DayStarts)
                                    {
                                        prepairMessageForNotification();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "prepairNotification");
            }
        }

        private void prepairMessageForNotification()
        {
            try
            {
                snoozeTime = 0;
                MovementListMdl.currentSelectedExerciseID = currentSelectedExercise.Id;
                message = string.Format("Your Exercise {0} is ready click on OK to continue!",
                currentSelectedExercise.MoveTitle);
                sendNotification(scheduleMdl.AlertTone);
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "prepairMessageForNotification");
            }
        }

        private void sendNotification(string AlertTone)
        {
            try
            {
                KeyguardManager myKM = (KeyguardManager)Android.App.Application.Context.GetSystemService(Context.KeyguardService);
                bool islock = myKM.InKeyguardRestrictedInputMode();
                if (islock)
                {
                    //it is locked
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                    {
                        MyAlarmManager();
                    }
                    else
                    {
                        AlartManager();
                    }
                }
                else
                {

                    AlartManager();
                    //it is not locked
                }

                notificationSound(AlertTone);
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "sendNotification");
            }
        }

        private void ExceptioHandler(string message, string method)
        {
            var notificationReceiver = new Exceptionalarmreceiver();
            //notificationReceiver.SetNotificationForException(DateTime.Now, "Warning!", "Your app has stoped! Restart your app to work as expected.");
            notificationReceiver.SetNotificationForException(DateTime.Now, "Warning!", method + " " + message);
            notificationSound(scheduleMdl.AlertTone);
        }
        private void AlartManager()
        {
            try
            {
                var h = new Handler(Looper.MainLooper);
                h.Post(() => {
                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                    alert.SetTitle(title);
                    alert.SetMessage(message);
                    alert.SetPositiveButton("Snooze", (senderAlert, args) =>
                    {
                        snoozeClickEvent();
                    });

                    alert.SetNegativeButton("Ok", (senderAlert, args) =>
                    {
                        Intent dialogIntent = new Intent(Android.App.Application.Context, typeof(okactivity));
                        dialogIntent.SetFlags(ActivityFlags.NewTask);
                        StartActivity(dialogIntent);
                    });
                    Dialog dialog = alert.Create();
                    dialog.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
                    dialog.Show();
                });
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "AlartManager");
            }
        }

        public static void snoozeClickEvent()
        {
            player.Pause();
            player.Stop();
            var manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            manager.Cancel(0);
            snoozeTime = snoozeTimeCurrentSelected;
        }

        private void MyAlarmManager()
        {
            try
            {
                var notificationReceiver = new alarmreceiver();
                notificationReceiver.SetNotification(DateTime.Now, title, message);
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "MyAlarmManager");
            }
        }

        private void notificationSound(string AlertTone)
        {
            try
            {
                Vibrator vibrator = (Vibrator)Android.App.Application.Context.GetSystemService(Context.VibratorService);
                if (vibrator.HasVibrator)
                {
                    vibrator.Vibrate(500);
                }
                if (AlertTone == Notification)
                {
                    player = MediaPlayer.Create(Android.App.Application.Context,
                        RingtoneManager.GetDefaultUri(RingtoneType.Notification));
                }
                else if (AlertTone == Alarm)
                {
                    player = MediaPlayer.Create(Android.App.Application.Context,
                        RingtoneManager.GetDefaultUri(RingtoneType.Alarm));
                }
                else
                {
                    player = MediaPlayer.Create(Android.App.Application.Context,
                        RingtoneManager.GetDefaultUri(RingtoneType.Ringtone));
                }
                player.Start();
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "notificationSound");
            }
        }

        public override void OnDestroy()
        {
            try
            {
                timer.Dispose();
                timer = null;
                isStarted = false;
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                base.OnDestroy();
                Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(SimpleStartedService)));
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "OnDestroy");
            }
        }

        void HandleTimerCallback(object state)
        {
            try
            {
                Count++;
                TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
                Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(SimpleStartedService)));
            }
            catch (System.Exception e)
            {
                ExceptioHandler(e.Message, "HandleTimerCallback");
            }
        }
    }
}