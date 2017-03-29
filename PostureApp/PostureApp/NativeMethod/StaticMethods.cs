using System;
using Xamarin.Forms;

namespace PostureApp.NativeMethod
{
    public static class  StaticMethods
    {
        public static void ShowToast(string msg)
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                DependencyService.Get<IIosMethods>().ShowToast(msg);
            }
            else
            {
                DependencyService.Get<IAndroidMethods>().ShowToast(msg);
            }

        }

        public static void startService()
        {
            if (Device.OS == TargetPlatform.iOS)
            {
               //DependencyService.Get<IServiceCaller>().CreateNotification();
				DependencyService.Get<IServiceCaller>().startService();
            }
            else
            {
                DependencyService.Get<IServiceCaller>().startService();
            }
        }

        //public static void ShowLoader()
        //{
        //    if (Device.OS == TargetPlatform.iOS)
        //    {
        //        DependencyService.Get<IIosMethods>().ShowLoader();
        //    }
        //    else
        //    {
        //        DependencyService.Get<IAndroidMethods>().ShowLoader();
        //    }

        //}
        //public static void DismissLoader()
        //{
        //    if (Device.OS == TargetPlatform.iOS)
        //    {
        //        DependencyService.Get<IIosMethods>().DismissLoader();
        //    }
        //    else
        //    {
        //        DependencyService.Get<IAndroidMethods>().DismissLoader();
        //    }

        //}



    }
}