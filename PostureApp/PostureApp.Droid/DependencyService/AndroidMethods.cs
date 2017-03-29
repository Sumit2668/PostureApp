using System;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using PostureApp.Droid.DependencyService;
using PostureApp.NativeMethod;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMethods))]
namespace PostureApp.Droid.DependencyService
{
    public class AndroidMethods:IAndroidMethods
    {
        Dialog dialog;
        public string GetIdentifier()
        {
            var id = Android.OS.Build.Serial;
            return id;
        }

        public void ShowToast(string msg)
        {
            Toast.MakeText(Forms.Context, msg, ToastLength.Short).Show();
        }
        
        //public void ShowLoader()
        //{
        //    dialog = new Dialog(Forms.Context);
        //    dialog.SetContentView(Resource.Layout.CustomProgressDialog);
           
        //    dialog.SetCancelable(false);
        //    dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
        //    dialog.Show();
        //}

        //public void DismissLoader()
        //{
        //    dialog.Dismiss();
        //}
    }
}