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
    public partial class ExePopupPage : PopupPage
    {
        private MovementListMdl obj = new MovementListMdl();
      //  DemoModel demoviewmodel = new DemoModel();
        public ExePopupPage()
        {
            InitializeComponent();
         //   BindingContext = demoviewmodel= new DemoModel();
            
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
                MovementListTbl tbl = await App.mvDatabase.GetItemAsync(MovementPage.SelectedExerciseid);
                if (tbl == null)
                {
                    tbl = new MovementListTbl();
                }
                obj = new MovementListMdl(tbl);
                ExeTitle.Text = obj.MoveTitle;
                ExeImage.Source = obj.ImageName;
                ExeDescription.Text = obj.Description;
                ExeDuration.Text = obj.Duration;
                SwitchActive.Source = obj.MoveIcon;
                if (obj.Selected == true)
                {
                    ExeActive.Text = "Select";
                }
                else
                {
                    ExeActive.Text = "Unselect";
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
        }


        async void On_Active(object sender, System.EventArgs e)
        {

            if (obj.Selected == false)
            {
                obj.MoveIcon = "on_btn.png";
                SwitchActive.Source = "on_btn.png";
                obj.Selected = true;
                ExeActive.Text = "Select";

            }
            else
            {
                obj.MoveIcon = "off_btn.png";
                SwitchActive.Source = "off_btn.png";
                obj.Selected = false;
                ExeActive.Text = "Unselect";
            }
            
            await App.mvDatabase.SaveItemAsync1(obj.GetTable());
        }

        async void Done_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
            await PopupNavigation.PopAsync(true);
        }

        private async void On_Close(object sender, EventArgs e)
        {
           // await Navigation.PushModalAsync(new MainPage());
            await PopupNavigation.PopAsync(true);
        }
    }
}
