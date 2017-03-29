using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AudioToolbox;
using Foundation;
using PostureApp.iOS.backgroundprocess;
using PostureApp.Model;
using PostureApp.NativeMethod;
using PostureApp.Views;
using UIKit;
using Xamarin.Forms;


[assembly: Dependency(typeof(backgroundservice))]
namespace PostureApp.iOS.backgroundprocess
{
	
	public class backgroundservice :IServiceCaller 
    {
		
        public string title = "Reminder";
        public string message = "Your exercise is ready click on OK to continue!";

        public ScheduleTbl scheduleTbl = new ScheduleTbl();
        public ScheduleMdl scheduleMdl = new ScheduleMdl();
        public List<MovementListTbl> movementListTbl = new List<MovementListTbl>();
        public static List<MovementListMdl> movementListMdl = new List<MovementListMdl>();
        public static List<MovementListMdl> movementListMdl_temp = new List<MovementListMdl>();
        public static MovementListMdl currentSelectedExercise = new MovementListMdl();
        //public static Windows.Media.Playback.MediaPlayer player;
        static readonly string TAG = "X:" + typeof(backgroundservice).Name;
        static readonly int TimerWait =  60 * 1/2;
        private static int snoozeTime = 0;
        private readonly static int snoozeTimeCurrentSelected = 5;


        Timer timer;
        DateTime startTime;

		NSTimer _nstimer;

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



     
	   public void StartBGProcess()
        {
            try
            {
				var sampleTimer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(TimerWait), delegate
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
                    }				
				});
            }
			catch (System.Exception e)
            {
                ExceptioHandler();
            }
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
                ExceptioHandler();
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
                ExceptioHandler();
            }
            return 1;
        }

        private async void prepairNotification()
        {
            try
            {
                await fillSchedule();
                await fillExercise();
				currentSelectedExercise = movementListMdl.FirstOrDefault();
				if (currentSelectedExercise != null)
                {
                    TimeSpan DayStarts = new TimeSpan(scheduleMdl.DayStarts.Ticks);
                    TimeSpan DayEnds = new TimeSpan(scheduleMdl.DayEnds.Ticks);
                    TimeSpan currenttime = DateTime.Now.TimeOfDay;
                    string currentDay = DateTime.Now.ToString("dddd");

					prepairMessageForNotification();

                    //if (DayStarts <= currenttime && DayEnds > currenttime)
                    //{
                    //    if ((currentDay == Sunday && scheduleMdl.Sun == true) || (currentDay == Monday && scheduleMdl.Mon == true) || (currentDay == Tuesday && scheduleMdl.Tue == true) || (currentDay == Wednesday && scheduleMdl.Wed == true) || (currentDay == Thursday && scheduleMdl.Thu == true) || (currentDay == Friday && scheduleMdl.Fri == true) || (currentDay == Saturday && scheduleMdl.Sat == true))
                    //    {
                    //        int reminder = (int)(currenttime.TotalMinutes % scheduleMdl.MoveFreq_Int);
                    //        if (reminder == 0 || currenttime == DayStarts)
                    //        {
                    //            if (snoozeTime > 0)//skip current selected exercise because it's time is over
                    //            {
                    //                backgroundservice.movementListMdl.Remove(backgroundservice.currentSelectedExercise);
                    //                await fillExercise();
                    //                currentSelectedExercise = movementListMdl.FirstOrDefault();
                    //            }

                    //            if (currentSelectedExercise != null)
                    //            {
                    //                prepairMessageForNotification();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (snoozeTime > 0)
                    //            {
                    //                reminder = (int)(currenttime.TotalMinutes % snoozeTime);
                    //                if (reminder == 0 || currenttime == DayStarts)
                    //                {
                    //                    prepairMessageForNotification();
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            catch (System.Exception e)
            {
                ExceptioHandler();
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
                ExceptioHandler();
            }
        }

	   private void sendNotification(string AlertTone)
        {
            try
            {
				

				//KeyguardManager myKM = (KeyguardManager)Android.App.Application.Context.GetSystemService(Context.KeyguardService);
				//if (myKM.InKeyguardRestrictedInputMode())
				//{
				//    //it is locked
				  MyAlarmManager();
				//}
				//else
				//{
			  //AlartManager();
				//    //it is not locked
				//}}
				//notificationSound(AlertTone);        
			}
            catch (System.Exception e)
            {
                ExceptioHandler();
            }
        }

        private void ExceptioHandler()
        {
           
			SetNotificationForException(DateTime.Now, "Warning!", "Your app has stoped! Restart your app to work as expected.");
            notificationSound(scheduleMdl.AlertTone);
        }

		public void SetNotificationForException(DateTime dateTime, string title, string message)
		{
			UIAlertView alert = new UIAlertView()
			{
				Title = title,
				Message = message
			};
			alert.AddButton("Ok");
			alert.Show();

			//StartWakefulService(Android.App.Application.Context, intent);
			//CompleteWakefulIntent(intent);

			//NofiticationReceiver.CompleteWakefulIntent(intent);
			//PendingIntent alarmIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, 0);

			var millsTillTriger = (long)dateTime.TimeOfDay.Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds;
			Task.Delay(TimeSpan.FromSeconds(millsTillTriger));

			//alarmMgr.Set(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + millsTillTriger,
			//alarmIntent);
		}

		private   void AlartManager()
        {
            try
            {
				UIAlertView alert = new UIAlertView()
				{
					Title = title,
					Message = message
				};
				alert.AddButton("Snooze");
				alert.AddButton("Ok");
				alert.Clicked += (object s, UIButtonEventArgs ev) =>
				{
					if (ev.ButtonIndex == 1)
					{
						//string[] arg = new string[1];
						//arg[0] = "ok delegate";
      					//PostureApp.iOS.Application.Main(arg);

						//MyNavigation nav = new MyNavigation();
						//nav.MyNavigation1();
						             
					}
					else
					{
						//player.Pause();
						//player.Stop();
						//var manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
						//manager.Cancel(0);
						snoozeTime = snoozeTimeCurrentSelected;
					}
				};
				alert.Show();
			}
            catch (System.Exception e)
            {
                ExceptioHandler();
            }
        }


		private void MyAlarmManager()
        {
            try
            {
               // SetNotification(DateTime.Now, title, message);

            }
            catch (System.Exception e)
            {
                ExceptioHandler();
            }
        }

		public void SetNotification(DateTime dateTime, string title, string message)
		{	
			
			var notification = new UILocalNotification();
			notification.FireDate = NSDate.FromTimeIntervalSinceNow(0);
			notification.AlertAction = title;
			notification.AlertBody = message;
			notification.ApplicationIconBadgeNumber = 1;
			notification.SoundName = UILocalNotification.DefaultSoundName;
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);

		
			//var millsTillTriger = (long)dateTime.TimeOfDay.Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds;

		}

		private void notificationSound(string AlertTone)
        {
            try
            {
				SystemSound.Vibrate.PlaySystemSound();

                //Vibrator vibrator = (Vibrator)Android.App.Application.Context.GetSystemService(Context.VibratorService);
        		//if (vibrator.HasVibrator)
                //{
                //  vibrator.Vibrate(500);
                //}

                //if (AlertTone == Notification)
                //{
                //  player = MediaPlayer.Create(Android.App.Application.Context,
                //  RingtoneManager.GetDefaultUri(RingtoneType.Notification));
                //}
                //else if (AlertTone == Alarm)
                //{
                //  player = MediaPlayer.Create(Android.App.Application.Context,
                //  RingtoneManager.GetDefaultUri(RingtoneType.Alarm));
                //}
                //else
                //{
                //  player = MediaPlayer.Create(Android.App.Application.Context,
                //  RingtoneManager.GetDefaultUri(RingtoneType.Ringtone));
                //}
                //player.Start();
            }
            catch (System.Exception e)
            {
                ExceptioHandler();
            }
        }

        //public override void OnDestroy()
        //{
        //    try
        //    {
        //        timer.Dispose();
        //        timer = null;
        //        isStarted = false;
        //        TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
        //        base.OnDestroy();
        //        Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(backgroundservice)));
        //    }
        //    catch (System.Exception e)
        //    {
        //        ExceptioHandler();
        //    }
        //}

        void HandleTimerCallback(object state)
        {
            try
            {
                Count++;
                TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
                //Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(backgroundservice)));
            }
            catch (System.Exception e)
            {
                ExceptioHandler();
            }
        }

        public void startService()
        {
			StartBGProcess();

        }
	}
}
