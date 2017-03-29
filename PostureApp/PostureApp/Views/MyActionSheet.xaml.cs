using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostureApp.Model;
using PostureApp.NativeMethod;
using PostureApp.Views;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace PostureApp
{
    public partial class MyActionSheet : PopupPage
    {
        ScheduleTbl obj;

        string selectnotification;
        public MyActionSheet(ScheduleTbl scobj)
        {
            InitializeComponent();
            obj = scobj;
        }
        public MyActionSheet()
        {
            InitializeComponent();
        }

        public async void SaveRecord()
        {
            try
            {
                obj.AlertTone = selectnotification;
                //StaticMethods.ShowToast("Data Saved");
                await App.scDatabase.SaveItemAsync1(obj);
            }
            catch (Exception e)
            {
                StaticMethods.ShowToast(e.Message);
            }
        }

        public async Task Appearing(View content, PopupPage page)
        {
            // Show animation
            await content.FadeTo(0.5);
        }

        // Call Before OnDisappering
        public async Task Disappearing(View content, PopupPage page)
        {
            // Hide animation
            await content.FadeTo(1);
        }


        private void DeRingtone_OnTapped(object sender, EventArgs e)
        {
            lblNotification.TextColor = lblAlarm.TextColor = Color.Gray;
            lblAlarm.FontSize = lblNotification.FontSize = 15;
            lblNotification.FontAttributes = lblAlarm.FontAttributes = FontAttributes.None;
            lblRingtone.TextColor = Color.FromHex("#4292f4");
            selectnotification = lblRingtone.Text;
            selectItem();
        }

        private void DeAlarm_OnTapped(object sender, EventArgs e)
        {
            lblNotification.TextColor = lblRingtone.TextColor = Color.Gray;
            lblNotification.FontSize = lblRingtone.FontSize = 15;
            lblNotification.FontAttributes = lblRingtone.FontAttributes = FontAttributes.None;

            lblAlarm.TextColor = Color.FromHex("#4292f4");
            selectnotification = lblAlarm.Text;
            selectItem();
        }

        private void DeNotification_OnTapped(object sender, EventArgs e)
        {
            lblAlarm.TextColor = lblRingtone.TextColor = Color.Gray;
            lblAlarm.FontSize = lblRingtone.FontSize = 15;
            lblRingtone.FontAttributes = lblAlarm.FontAttributes = FontAttributes.None;
            lblNotification.TextColor = Color.FromHex("#4292f4");
            selectnotification = lblNotification.Text;
            selectItem();
        }

        private void selectItem()
        {
            if (lblRingtone.TextColor == Color.FromHex("#4292f4"))
            {
                lblRingtone.FontSize = 17;
                lblRingtone.FontAttributes = FontAttributes.Bold;

            }
            else if (lblAlarm.TextColor == Color.FromHex("#4292f4"))
            {
                lblAlarm.FontSize = 17;
                lblAlarm.FontAttributes = FontAttributes.Bold;


            }
            else
            {
                lblNotification.FontSize = 17;
                lblNotification.FontAttributes = FontAttributes.Bold;
                

            }
        }
        async void OK_Clicked(object sender, System.EventArgs e)
        {
            SaveRecord();
          
            await PopupNavigation.PopAsync(true);
        }

        async void Cancel_Clicked(object sender, System.EventArgs e)
        {   
           
            await PopupNavigation.PopAsync(true);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                ((App)App.Current).ResumeAtScheId = -1;
                obj = await App.scDatabase.GetItemsAsync();
                ScheduleMdl scheduleMdl = ScheduleMdl.PrepareList(obj);
                selectnotification = scheduleMdl.AlertTone;
                if (selectnotification == "Default Ringtone")
                {
                    lblRingtone.FontSize = 17;
                    lblRingtone.FontAttributes = FontAttributes.Bold;
                    lblRingtone.TextColor = Color.FromHex("#4292f4");
                    lblNotification.FontSize = lblAlarm.FontSize = 15;
                }
                else if (selectnotification == "Default Alarm")
                {
                    lblAlarm.FontSize = 17;
                    lblAlarm.FontAttributes = FontAttributes.Bold;
                    lblAlarm.TextColor = Color.FromHex("#4292f4");
                    lblNotification.FontSize = lblRingtone.FontSize = 15;
                }
                else
                {
                    lblNotification.FontSize = 17;
                    lblNotification.FontAttributes = FontAttributes.Bold;
                    lblNotification.TextColor = Color.FromHex("#4292f4");
                    lblAlarm.FontSize = lblRingtone.FontSize = 15;
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
        }
    }
}
