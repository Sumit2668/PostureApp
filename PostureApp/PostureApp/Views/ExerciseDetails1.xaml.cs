using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using PostureApp.Model;
using PostureApp.NativeMethod;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;

namespace PostureApp.Views
{
    public partial class ExerciseDetails1 : PopupPage
    {
        private MovementListMdl obj = new MovementListMdl();
        public ExerciseDetails1()
        {
            InitializeComponent();
        }
        public async Task Appearing(View content, PopupPage page)
        {
            await content.FadeTo(0.5);
        }

        public async Task Disappearing(View content, PopupPage page)
        {
            await content.FadeTo(0);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {

                MovementListTbl tbl = await App.mvDatabase.GetItemAsync(MovementListMdl.currentSelectedExerciseID);
                if (tbl == null)
                {
                    tbl = new MovementListTbl();
                }
                obj = new MovementListMdl(tbl);


                ExeTitle.Text = obj.MoveTitle;
                ExeImage.Source = obj.ImageName;
                ExeDescription.Text = obj.Description;
                ExeDuration.Text = obj.Duration;
            }
            catch (Exception ex)
            {

            }
        }

        private async void Dismiss_OnTapped(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.PopAsync(true);
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            
        }

        private async void Done_OnTapped(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.PopAsync(true);
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
        }
    }
}
