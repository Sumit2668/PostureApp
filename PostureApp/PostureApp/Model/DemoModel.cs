using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PostureApp.Model
{
   public class DemoModel : INotifyPropertyChanged
    {
        public ObservableCollection<MovementListMdl> Items { get; set; }
        public DemoModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged( string propertyname)
        {
            if (PropertyChanged==null)
                return;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        private string moveIcon;
        public string MoveIcon
        {
            get
            {
                return moveIcon;
            }
            set
            {
                moveIcon = value;
                OnPropertyChanged("MoveIcon");
                
            }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set {
                selected = value;               
                OnPropertyChanged("Selected");
                
            }
        }
        bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                OnPropertyChanged("IsBusy");
                
            }
        }

        public string active;
        public string Active
        {
            get { return active; }
            set { active = value;
                if (Selected == false)
                {                   
                    Active = "Select";
                }
                else
                {                   
                    Active = "Unselect";
                }
                OnPropertyChanged("Active");
            }
        }


        ICommand refreshCommand;
        public ICommand RefreshCommand
        {get { return refreshCommand ??
                    (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)

                return;
            IsBusy = true;
            IsBusy = false;
        }


        public int Id { get; set; }
        public string MoveTitle { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
         public string Duration { get; set; }

        public static int currentSelectedExerciseID { get; set; }

        public MovementListTbl GetTable()
        {
            MovementListTbl movementListTbl = new MovementListTbl();
            movementListTbl.Id = Id;
            movementListTbl.MoveTitle = MoveTitle;
            movementListTbl.Selected = Selected;
            movementListTbl.MoveIcon = MoveIcon;
            movementListTbl.ImageName = ImageName;
            movementListTbl.Description = Description;
            movementListTbl.Duration = Duration;
            return movementListTbl;
        }

    }
}
