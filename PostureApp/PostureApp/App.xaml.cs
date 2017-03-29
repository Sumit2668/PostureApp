using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostureApp.NativeMethod;
using PostureApp.SQLite;
using PostureApp.Views;
using Xamarin.Forms;

namespace PostureApp
{
    public partial class App : Application
    {
       // public static int ScreenWidth { get; internal set; }

        
        // these is for MovementDB
        public int ResumeAtMoveId { get; set; }
        static MovementDB mvdatabase;
        public static MovementDB mvDatabase
        {
            get
            {
                if (mvdatabase == null)
                {
                    mvdatabase = new MovementDB(DependencyService.Get<ISQLiteHelper>().GetLocalFilePath("Posture.db3"));
                }
                return mvdatabase;
            }
        }

        // these is for ScheduleDB
        public int ResumeAtScheId { get; set; }
        static ScheduleDB scdatabase;
        public static ScheduleDB scDatabase
        {
            get
            {
                if (scdatabase == null)
                {
                    scdatabase = new ScheduleDB(DependencyService.Get<ISQLiteHelper>().GetLocalFilePath("Posture.db3"));
                }
                return scdatabase;
            }
        }

        public static object Instance { get; set; }

        public App()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.Android)
            {
                MainPage = new NavigationPage(new StartScreen());
                //MainPage = new NavigationPage(new DemoMovePage());
            }
            else
            {
                MainPage = new NavigationPage(new PostureSettingPage());
            }
            
            
        }
        public App(string viewName)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ExerciseDetails1());
           
		}


		public static Page  GetSecondPageIOS()
		{
			var formsPage = new NavigationPage(new ExerciseDetails1());
     		return formsPage;
		}


		protected override void OnStart()
        {
			StaticMethods.startService();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps         
        }

        protected override void OnResume()
        {          
            // Handle when your app resumes
        }

}
}
