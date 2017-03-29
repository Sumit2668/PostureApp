using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostureApp.Model;
using Xamarin.Forms;

namespace PostureApp.Views
{
    public partial class PostureSetDemo : ContentPage
    {
        public PostureSetDemo()
        {
            InitializeComponent();
        }

        private async void Insert_OnClicked(object sender, EventArgs e)
        {
            var obj = (MovementListTbl)BindingContext;

            //MovementListTbl obj = new MovementListTbl();
            //obj.MoveTitle = exTitle.Text;

            if (obj.Selected != exSelect.IsToggled)
            { obj.MoveIcon = "on_btn.png"; }
            else
            {
                obj.MoveIcon = "off_btn.png";
            }

            await App.mvDatabase.SaveItemAsync1(obj);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (MovementListTbl)BindingContext;
            await App.mvDatabase.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }



        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    // Reset the 'resume' id, since we just want to re-start here
        //    //((App)App.Current).ResumeAtTodoId = -1;
        //    List<ExerciseMaster> lst = await App.exDatabase.GetItemsAsync();
        //    exTitle.Text = lst[0].Title;
        //}
    }
}
