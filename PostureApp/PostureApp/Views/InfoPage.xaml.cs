using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Plugin.Messaging;
using PostureApp.NativeMethod;

namespace PostureApp.Views
{
    public partial class InfoPage : BaseContentPage
    {
        public InfoPage()
        {
           InitializeComponent();
        }

        void Email_Tapped(object sender, System.EventArgs e)
        {
            var EmailTo = "sumit.sisodia@arnasoftech.com";
            var EmailSubject = "Subject: PostureApp";
            var EmailBody = "Body: Posture App for Exercise";

            var EmailTask = MessagingPlugin.EmailMessenger;

            if (EmailTask.CanSendEmail)
                EmailTask.SendEmail(EmailTo, EmailSubject, EmailBody);
            StaticMethods.ShowToast("Send Mail Successfully.");
        }
    }
}
