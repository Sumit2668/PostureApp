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
using Xamarin.Forms;
using PostureApp.Droid.backgroundprocess;
using Android.Media;
using Java.Lang;
using PostureApp.NativeMethod;
using PostureApp.Model;
using System.Threading.Tasks;

[assembly: Dependency(typeof(backgroundservice))]
namespace PostureApp.Droid.backgroundprocess
{
    [Service]
    public class backgroundservice : Service
    {
        //Update 22 March
        public string title = "Reminder";
        public string message = "Your exercise is ready click on OK to continue!";
        
        public  ScheduleTbl scheduleTbl = new ScheduleTbl();
        public ScheduleMdl scheduleMdl = new ScheduleMdl();
        public List<MovementListTbl> movementListTbl = new List<MovementListTbl>();
        public static List<MovementListMdl> movementListMdl = new List<MovementListMdl>();
        public static List<MovementListMdl> movementListMdl_temp = new List<MovementListMdl>();
        public static MovementListMdl currentSelectedExercise = new MovementListMdl();
        public static MediaPlayer player;
        static readonly string TAG = "X:" + typeof(backgroundservice).Name;
        static readonly int TimerWait = 1000 * 60 * 1;
        private static int snoozeTime = 0;
        private readonly static int snoozeTimeCurrentSelected = 5;
        
        Timer timer;
        DateTime startTime;
        bool isStarted = false;

        private int tempCount = 0;
        private int Count = 0;

        readonly string  Monday = "Monday";
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
             Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(backgroundservice)));
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            try
            {
               // new Task(() => {

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
                    }

              //  }).Start();
            }
            catch (System.Exception e)
            {
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
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
                if (movementListMdl.Count == 0)
                {
                    movementListTbl = await App.mvDatabase.GetItemsAsync().ConfigureAwait(false);
                    foreach (var item in movementListTbl.Where(ml => ml.Selected == true))
                    {
                        movementListMdl.Add(new MovementListMdl(item));
                    }
                }
                else
                {
                    movementListTbl = await App.mvDatabase.GetItemsAsync().ConfigureAwait(false);
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
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
            }

            return 1;
        }

        private async Task<int> fillSchedule()
        {
          try
          {            
             scheduleTbl = await App.scDatabase.GetItemsAsync().ConfigureAwait(false);
	          if (scheduleTbl == null)
	          {
	              scheduleTbl = new ScheduleTbl();
	          }
	          scheduleMdl = ScheduleMdl.PrepareList(scheduleTbl);
	          scheduleMdl.MoveFreq_Int = scheduleMdl.GetFrequency();
	          }
            catch (System.Exception e)
	        {
	            Toast.MakeText(this,e.Message,ToastLength.Long).Show();
	        }
           return 1;
        }

        private async void prepairNotification()
        {
            try
            {            
            
            await fillExercise();
            currentSelectedExercise = movementListMdl.FirstOrDefault();
            if (currentSelectedExercise != null)
            {
                    await fillSchedule();
                    TimeSpan  DayStarts = new TimeSpan(scheduleMdl.DayStarts.Ticks);
                TimeSpan DayEnds = new TimeSpan(scheduleMdl.DayEnds.Ticks);
                TimeSpan currenttime =  DateTime.Now.TimeOfDay;
                string currentDay = DateTime.Now.ToString("dddd");
      
                if (DayStarts <= currenttime && DayEnds > currenttime)
                {
                        if ((currentDay == Sunday && scheduleMdl.Sun == true) || (currentDay == Monday && scheduleMdl.Mon == true) || (currentDay == Tuesday && scheduleMdl.Tue == true) || (currentDay == Wednesday && scheduleMdl.Wed == true) || (currentDay == Thursday && scheduleMdl.Thu == true) || (currentDay == Friday && scheduleMdl.Fri == true) || (currentDay == Saturday && scheduleMdl.Sat == true))
                        {
                                //int reminder = (int)(currenttime.TotalMinutes % scheduleMdl.MoveFreq_Int);
                            int reminder = 0;
                            if (reminder == 0 || currenttime == DayStarts)
                                {
                                        if (snoozeTime > 0)//skip current selected exercise because it's time is over
                                        {
                                                backgroundservice.movementListMdl.Remove(backgroundservice.currentSelectedExercise);
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
                                                reminder = (int) (currenttime.TotalMinutes % snoozeTime);
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
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
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
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
            }
        }

        private void sendNotification(string AlertTone)
        {
            try
            {           
            KeyguardManager myKM = (KeyguardManager)Android.App.Application.Context.GetSystemService(Context.KeyguardService);
            if (myKM.InKeyguardRestrictedInputMode())
            {
                //it is locked
                MyAlarmManager();
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
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
            }
        }

        private void ExceptioHandler()
        {
            try
            {
                var notificationReceiver = new Exceptionalarmreceiver();
                notificationReceiver.SetNotificationForException(DateTime.Now, "Warning!", "Your app has stoped! Restart your app to work as expected.");
                notificationSound(scheduleMdl.AlertTone);
            }
            catch (System.Exception e)
            {
                Toast.MakeText(this,e.Message,ToastLength.Short).Show();
            }
        }

        private  void AlartManager()
        {
            try
            {            
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
            }
            catch (System.Exception e)
            {
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
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
            notificationReceiver.SetNotification(DateTime.Now,title, message);
            }
            catch (System.Exception e)
            {
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
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
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
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
            Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(backgroundservice)));
            base.OnDestroy();

            }
            catch (System.Exception e)
            {
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
            }
        }

        void HandleTimerCallback(object state)
        {
            try
            {
             Count++;
             TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
             Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(backgroundservice)));
            }
            catch (System.Exception e)
            {
                Toast.MakeText(this,e.Message,ToastLength.Long).Show();
            }
        }
    }
}