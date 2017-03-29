using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostureApp.Model;
using PostureApp.Views;
using Xamarin.Forms;

namespace PostureApp
{
    public partial class MainPage : TabbedPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Device.BeginInvokeOnMainThread(async () => {
                 Navigation.PushModalAsync(new StartScreen());
            });
            return true;
        }
    }
}

