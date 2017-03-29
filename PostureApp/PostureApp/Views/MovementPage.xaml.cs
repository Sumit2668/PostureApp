using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostureApp.Model;
using PostureApp.NativeMethod;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace PostureApp.Views
{
    public partial class MovementPage : BaseContentPage
    {
        public static int SelectedExerciseid = 0;
        public  MovementPage()
        {
            InitializeComponent();
            //  BindingContext = new DemoModel();
        }

        private async void SwitchButton_OnTapped(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Forms.Image img = (Xamarin.Forms.Image)sender;
                Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)img.Source;
                MovementListTbl obj = (MovementListTbl)objFileImageSource.BindingContext;

                // Access the file that was specified:-
                string strFileName = objFileImageSource.File;
                if (strFileName == "off_btn.png")
                {
                    obj.MoveIcon = "on_btn.png";
                    img.Source = "on_btn.png";
                    obj.Selected = true;
                }
                else
                {
                    obj.MoveIcon = "off_btn.png";
                    img.Source = "off_btn.png";
                    obj.Selected = false;
                }

                await App.mvDatabase.SaveItemAsync1(obj);
                // await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
        }
        private async void ExerciseTitle_OnTapped(object sender, EventArgs e)
        {
            Xamarin.Forms.Label lbl = (Xamarin.Forms.Label)sender;
            MovementListTbl obj = (MovementListTbl)lbl.BindingContext;
            SelectedExerciseid = obj.Id;
            await Navigation.PushPopupAsync(new ExePopupPage());
        }

        void MoveList_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
         
        }
     
        public async void LoadData()
        {
            ObservableCollection<MovementListMdl> movementListMdlList = null;
            
            MainListView.ItemsSource = null;
            try
            {
                // Reset the 'resume' id, since we just want to re-start here
                ((App)App.Current).ResumeAtMoveId = -1;
                List<MovementListTbl> movementListTbl = await App.mvDatabase.GetItemsAsync();
                //foreach (var item in movementListTbl)
                //{
                //    await App.mvDatabase.DeleteItemAsync(item);
                //}
                if (movementListTbl.Count == 0)
                {
                    movementListMdlList = MovementListMdl.GetPerdefinedList();
                    foreach (MovementListMdl movementListMdl in movementListMdlList)
                    {
                        await App.mvDatabase.SaveItemAsync1(movementListMdl.GetTable());
                    }
                    movementListTbl = await App.mvDatabase.GetItemsAsync();
                }
                MainListView.ItemsSource = movementListTbl;
               
            }
            catch (Exception ex)
            {

                StaticMethods.ShowToast(ex.Message);
            }
            finally
            {
                movementListMdlList = null;
            }
        }
    }
}
