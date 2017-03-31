using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PostureApp.Droid.backgroundprocess
{

    [Activity(Label = "PostureApp",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class okactivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            var manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            manager.Cancel(0);
            SimpleStartedService.movementListMdl.Remove(SimpleStartedService.currentSelectedExercise);
            if (SimpleStartedService.player != null)
            {
                SimpleStartedService.player.Pause();
                SimpleStartedService.player.Stop();
            }
            LoadApplication(new App("info"));            
        }
    }
}