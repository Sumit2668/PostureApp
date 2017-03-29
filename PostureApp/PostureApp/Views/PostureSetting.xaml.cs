using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PostureApp.Model;
using PostureApp.NativeMethod;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace PostureApp.Views
{
    public partial class PostureSetting :  PopupPage
    {
        ScheduleTbl obj = new ScheduleTbl();
        int TimeDiff;
        string selectnotification;

        public PostureSetting()
        {
            InitializeComponent();
        }

		public async Task Appearing(View content, PopupPage page)
		{
			// Show animation
				await content.FadeTo(1);
		}

		// Call Before OnDisappering
		public async Task Disappearing(View content, PopupPage page)
		{
			// Hide animation
					await content.FadeTo(0);
		}

		public async void SaveRecord()
        {
            try
            {
                obj.MoveFreq = Spanhour.Text;
                obj.DayStarts = new DateTime(MyStatsTimePicker.Time.Ticks);
                obj.DayEnds = new DateTime(MyEndsTimePicker.Time.Ticks);
                if (selectnotification==null)
                {
                    obj.AlertTone = "Default Ringtone";
                }
                //StaticMethods.ShowToast("Data Saved");
                await App.scDatabase.SaveItemAsync1(obj);

            }
            catch (Exception e)
            {
				StaticMethods.ShowToast(e.Message);
            }
        }
        
        void StartTime_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            SaveRecord();
        }

        void EndsTime_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            SaveRecord();
        }

        private async void SunImage_OnTapped(object sender, EventArgs e)
        {
            if (obj.Sun == true)
            {
                obj.Sun = false;
                LblSu.TextColor =       Color.FromHex("#FFFFFF");
                LblSu.BackgroundColor=  Color.FromHex("#00858f");
            }   
            else
            {
                obj.Sun = true;       
                LblSu.TextColor =       Color.FromHex("#00858f");
                LblSu.BackgroundColor = Color.FromHex("#FFFFFF");
            }
           
                SaveRecord();
        }

        private async void MnImage_OnTapped(object sender, EventArgs e)
        {
            if (obj.Mon == true)
            {
                obj.Mon = false;
                LblMn.TextColor =       Color.FromHex("#FFFFFF");
                LblMn.BackgroundColor = Color.FromHex("#00858f");
            }   
            else
            {
                obj.Mon = true;         
                LblMn.TextColor =       Color.FromHex("#00858f");
                LblMn.BackgroundColor = Color.FromHex("#FFFFFF");
            }
           
            SaveRecord();
        }

        private async void TuImage_OnTapped(object sender, EventArgs e)
        {
            if (obj.Tue == true)
            {
                obj.Tue = false;
                LblTu.TextColor =       Color.FromHex("#FFFFFF");
                LblTu.BackgroundColor = Color.FromHex("#00858f");
            }   
            else
            {
                obj.Tue = true;
                LblTu.TextColor =       Color.FromHex("#00858f");
                LblTu.BackgroundColor = Color.FromHex("#FFFFFF");
            }
                
            SaveRecord();
        }

        private  async void WeImage_OnTapped(object sender, EventArgs e)
        {
            if (obj.Wed == true)
            {
                obj.Wed = false;
                LblWe.TextColor =       Color.FromHex("#FFFFFF");
                LblWe.BackgroundColor = Color.FromHex("#00858f");
            }        
            else
            {
                obj.Wed = true;
                LblWe.TextColor =       Color.FromHex("#00858f");
                LblWe.BackgroundColor = Color.FromHex("#FFFFFF");
            }
            SaveRecord();
        }

        private async void ThImage_OnTapped(object sender, EventArgs e)
        {
            if (obj.Thu == true)
            {
                obj.Thu = false;
                LblTh.TextColor =       Color.FromHex("#FFFFFF");
                LblTh.BackgroundColor = Color.FromHex("#00858f");
            }        
            else     
            {
                obj.Thu = true;
                LblTh.TextColor =       Color.FromHex("#00858f");
                LblTh.BackgroundColor = Color.FromHex("#FFFFFF");
            }

       
            SaveRecord();
        }

        private async void FrImage_OnTapped(object sender, EventArgs e)
        {
            if (obj.Fri == true)
            {
                obj.Fri = false;
                LblFr.TextColor =       Color.FromHex("#FFFFFF");
                LblFr.BackgroundColor = Color.FromHex("#00858f");
            }       
            else
            {
                obj.Fri = true;
                LblFr.TextColor =       Color.FromHex("#00858f");
                LblFr.BackgroundColor = Color.FromHex("#FFFFFF");
            }
            
            SaveRecord();
        }

        private async void SaImage_OnTapped(object sender, EventArgs e)
        {
            if (obj.Sat == true)
            {
                obj.Sat = false;
                LblSa.TextColor =       Color.FromHex("#FFFFFF");
                LblSa.BackgroundColor = Color.FromHex("#00858f");
            }        
            else     
            {
                obj.Sat = true;
                LblSa.TextColor =       Color.FromHex("#00858f");
                LblSa.BackgroundColor = Color.FromHex("#FFFFFF");
            }
           
            SaveRecord();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                // Reset the 'resume' id, since we just want to re-start here
                ((App)App.Current).ResumeAtScheId = -1;
                // ScheduleMdl sch = await App.scDatabase.GetItemsAsync();
                obj = await App.scDatabase.GetItemsAsync();
                if (obj == null)
                {
                    obj = new ScheduleTbl();
                }
                ScheduleMdl scheduleMdl = ScheduleMdl.PrepareList(obj);
                MyStatsTimePicker.Time = new TimeSpan(scheduleMdl.DayStarts.Ticks);
                MyEndsTimePicker.Time = new TimeSpan(scheduleMdl.DayEnds.Ticks);
                Spanhour.Text = scheduleMdl.MoveFreq;

                LblSu.BackgroundColor = scheduleMdl.sunBgColor;
                LblSu.TextColor= scheduleMdl.sunTxtColor;

                LblMn.BackgroundColor = scheduleMdl.monBgColor;
                LblMn.TextColor = scheduleMdl.monTxtColor;

                LblTu.BackgroundColor = scheduleMdl.tueBgColor;
                LblTu.TextColor = scheduleMdl.tueTxtColor;

                LblWe.BackgroundColor = scheduleMdl.wedBgColor;
                LblWe.TextColor = scheduleMdl.wedTxtColor;

                LblTh.BackgroundColor = scheduleMdl.thuBgColor;
                LblTh.TextColor = scheduleMdl.thuTxtColor;

                LblFr.BackgroundColor = scheduleMdl.friBgColor;
                LblFr.TextColor = scheduleMdl.friTxtColor;

                LblSa.BackgroundColor = scheduleMdl.satBgColor;
                LblSa.TextColor = scheduleMdl.satTxtColor;

                MySliderhour.Value = scheduleMdl.GetFrequency();
            }
            catch (Exception ex)
            {
				StaticMethods.ShowToast(ex.Message);
            }


        }

        private void SettingImage_OnTapped(object sender, EventArgs e)
        {

        }

        private async void MovmentImage_OnTapped(object sender, EventArgs e)
        {
           // Navigation.InsertPageBefore(new PostureSetting(), this);
            //var detail = new MovementListPage();
            //await Navigation.PushPopupAsync(detail);
            await PopupNavigation.PushAsync(new MovementListPage());
        }

        private async void InfoImage_OnTapped(object sender, EventArgs e)
        {
           // Navigation.InsertPageBefore(new PostureSetting(), this);
           // await Navigation.PushAsync(new InfoPage());
           // await PopupNavigation.PushAsync(new InfoPage());
        }

        private async void MyMoveImage_OnTapped(object sender, EventArgs e)
        {
            SaveRecord();
           // Navigation.InsertPageBefore(new PostureSetting(), this);
           // await Navigation.PushPopupAsync(new PostureSetting());
            // or
            await PopupNavigation.PushAsync(new MovementListPage());
        }        

        private void MySliderhour_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (MySliderhour.Value <= 15)
            {
                Spanhour.Text = "15 Min";
            }
            else if (MySliderhour.Value <= 30)
            {
                Spanhour.Text = "30 Min";
            }
            else if (MySliderhour.Value <= 45)
            {
                Spanhour.Text = "45 Min";
            }
            else if (MySliderhour.Value <= 60)
            {
                Spanhour.Text = "hour";
            }
            else if (MySliderhour.Value <= 90)
            {
                Spanhour.Text = "90 Min";
            }
            else if (MySliderhour.Value <= 120)
            {
                Spanhour.Text = "2 hour";
            }
            else if (MySliderhour.Value <= 180)
            {
                Spanhour.Text = "3 hour";
            }
            else if (MySliderhour.Value <= 240)
            {
                Spanhour.Text = "4 hour";
            }
            SaveRecord();
        }
        
        async void SetAlert_OnTapped(object sender, System.EventArgs e)
        {
           
            await Navigation.PushPopupAsync(new MyActionSheet());
            //Navigation.PushAsync(new LoginPage());
        }

        private void MySlider_OnUnfocused(object sender, FocusEventArgs e)
        {
            SaveRecord();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}



//Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)ImageSu.Source;
//string strFileName = objFileImageSource.File;
//ScheduleMdl obj = (ScheduleMdl)objFileImageSource.BindingContext;