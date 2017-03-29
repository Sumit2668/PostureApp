using System;
using BigTed;
using PostureApp.iOS.DependencyService;
using PostureApp.NativeMethod;

[assembly: Xamarin.Forms.Dependency(typeof(IosMethods))]

namespace PostureApp.iOS.DependencyService
{
    public class IosMethods:IIosMethods
	{
        private string Key = "abc";
        
		public IosMethods()
        {
        }

        public string GetIdentifier()
        {
            var id = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            return id;
        }

        public void ShowToast(string msg)
        {
            BTProgressHUD.ShowToast(msg, false, 50);
        }
        public void ShowLoader()
        {
            BTProgressHUD.Show();
        }
        public void DismissLoader()
        {
            BTProgressHUD.Dismiss();
        }
      
      
    }
}