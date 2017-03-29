using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using PostureApp.NativeMethod;

namespace PostureApp.Views
{
    public partial class StartScreen : PopupPage
    {
        public StartScreen()
        {
            InitializeComponent();
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
			await content.FadeTo(0);
		}

        private async void bgImage_OnTapped(object sender, EventArgs e)
        {
            try
            {
                var detailsPage = new MainPage();
                await Navigation.PushModalAsync(new MainPage());
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
           
        }
    }
}
