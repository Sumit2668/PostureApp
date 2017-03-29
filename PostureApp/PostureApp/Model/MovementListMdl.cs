using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PostureApp.Model
{
    public class MovementListMdl : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MovementListMdl()
        {

        }


        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
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
                NotifyPropertyChanged();
            }
        }

        public MovementListMdl(MovementListTbl mdl)
        {
            Id = mdl.Id;
            MoveTitle = mdl.MoveTitle;
            ImageName = mdl.ImageName;
            Description = mdl.Description;
            Selected = mdl.Selected;
            MoveIcon = mdl.MoveIcon;
            Duration = mdl.Duration;
        }

        public int Id { get; set; }
        public string MoveTitle { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }
        // public string MoveIcon { get; set; }
        public string Duration { get; set; }

        public static int currentSelectedExerciseID { get; set; }



        public static ObservableCollection<MovementListMdl> GetPerdefinedList()
        {
            ObservableCollection<MovementListMdl> movementListMdl = new ObservableCollection<MovementListMdl>();

            movementListMdl.Add(new MovementListMdl { MoveTitle = "Palms-Up Swan", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand tall, pressing the top of your head toward the ceiling. Reach your arms out in front of you, palms up. Keeping your arms straight, spread them apart opening your chest. Point your thumbs behind, pulling your shoulder blades together. Hold for :30 then relax.", Duration = "Hold for 30 seconds" });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Jefferson Curl", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand as tall as possible. Start by tucking your chin to your chest. Rolling your shoulders forward, curl your entire upper body down toward your feet. Keep your legs straight and chin tucked down. Uncurl yourself starting at the low back, ending by lifting the chin and standing tall. Each repitition should take at least 10 seconds.", Duration = "Repeat 5 times." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Runner’s Stretch", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Hold for 30 seconds on each side.\rStand and use a wall, desk or chair for balance. Pull your right foot toward your butt and hold onto it with your right hand. Focus on lifting your chest and squeezing your but to push your hip forward. Repeat on left leg.", Duration = "Hold for 30 seconds on each side." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Standing Calf Stretch", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand about 3 feet from a wall with both hands on the wall for support.\rWith your heel 2-3” off the ground, press the ball of your foot onto the wall. Press your heel toward the wall, keeping your knee straight. You heel should now be touching the floor. Push your hips toward the wall to add more stretch. Repeat on left side.", Duration = "Hold for 30 seconds on each side." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "The Executive", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Sit toward the front edge of a chair. Make yourself tall with a straight back and lifted chest. Cross your right ankle onto your left knee. Keeping your back straight, lean forward pressing your chest toward your right knee. Repeat on left side.", Duration = "Hold for 30 each side." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Casual Hamstring", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand tall, back straight, chest lifted. Place your right heel on a desk or chair at a height that you are able to keep your knee straight. Keeping your back flat, lean forward at the hips. Press your chest toward your kneed. Repeat on left side.", Duration = "Hold for 30 each side." });

            movementListMdl.Add(new MovementListMdl { MoveTitle = "Standing Sink", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand tall, chest lifted. Interlace your fingers behind your back, palms\rfacing upward. Keeping your elbows straight, lift your arms away from\ryour back. Focus on reaching away from yourself and lifting your chest.", Duration = "4 repetitions, :10 each" });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Samson Lunge", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Step forward with your right leg. Keeping your left leg straight, lower\ryourself straight down as far as possible. If able, reach your arms straight\roverhead toward the ceiling. Focus on staying upright.", Duration = ":30 hold on each leg" });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Finger Laced Rhomboid", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Interlace your fingers, palms facing the ground. Lift your elbows toward\rthe ceiling, fingers under your chin and inhale. Tuck your knuckles toward your chest and press your elbows forward and toward each other. Exhale and hold for :10. Repeat from the beginning.", Duration = "5 repetitions. :10 hold." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Sprinter", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Place both hands on a wall, standing about 3 or 4 feet away from it. Bend your left leg taking a small step toward the wall. Keeping your right heel firmly on the floor, bend both elbows and squeeze your butt pushing your hips toward the wall. Repeat on left leg.", Duration = ":30 hold on each leg." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Sky Reach", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand tall. Extend your arms overhead and lift your head and chest toward the ceiling. Squeeze your butt to push your hips forward. To enhance the movement, try interlacing your fingers with palms facing up.", Duration = "30 hold" });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Arm Swings", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Standing tall, start by crossing your arms across your chest. Open both arms out to your sides at chest level. Then bring them back across your chest in a swinging motion. With each rep, try to cross over and open up a bit further.", Duration = "20 repetitions." });

            movementListMdl.Add(new MovementListMdl { MoveTitle = "Hip Circles", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand tall, hands on your hips. Squeeze your butt to push your hips forward. Then shift your hips to your right in a circular motions. Bring the circle all the way around for 10 repetitions, then repeat for 10 in the opposite direction.", Duration = "10 repetitions each direction." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Kick Backs", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Using a desk or chair for support, extend your right leg upward behind you. Focus on pulling your toes toward your knee and pushing your heel up and back. Keep your knee extended and your chest upright.", Duration = "15 repetitions on each leg" });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Scap Row", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Seated or standing. Extend your arms out straight in front of you at chest height, palms down. Keeping hands pointing forward, bend at the elbows and “row” them back squeezing your shoulder blades together. Keeping your elbows in place, rotate your arms up to point at the ceiling. Hold for :05.", Duration = "10 repetitions, each held for :05." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Wall Lat Reach", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand facing a wall and reach your arm overhead, placing your right hand flat on the wall. Step your right leg away from the wall. Rotate your right shoulder slightly toward the wall. Bending your left leg slightly and lowering your body will enhance the movement.", Duration = ":30 hold on each side." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Pec Opener", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Standing facing a wall. Extend your left arm to your side at shoulder height, hand flat against the wall. Keeping your left shoulder pressed against the wall, rotate your right shoulder away from the wall.\r", Duration = "30 hold on each side." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Standing Hip Push", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Standing, place the heels of your palms on your back at your beltline with fingers pointing downward. Squeeze your elbows together and push your hips forward. Lift your chin.", Duration = "30 hold." });

            movementListMdl.Add(new MovementListMdl { MoveTitle = "Chair Squat", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Sit toward the front edge of a chair or similar height surface. Bring your feet slightly behind your knees and press your weight into your heels. Stand up, making sure to keep your knees pressed out slightly and your chest lifted. Sit back down the same way. Seating surface height will affect difficulty.", Duration = "20 repetitions." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Heel Toe Rocks", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand tall. Lift your toes slightly off the floor, shifting weight to your heels. Then rock your weight forward into your toes, lifting your heels off the ground.", Duration = "30 repetitions." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Backhand Opener", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand at a desk or similar surface. Bend forward, pointing your hands toward each other with bent elbows. Press the backs of your hands and wrists down and begin to straighten your elbows. Extend to desired position and hold.", Duration = ":30 hold." });
            movementListMdl.Add(new MovementListMdl { MoveTitle = "Forehand Opener", Selected = true, MoveIcon = "on_btn.png", ImageName = "exercise.png", Description = "Stand at a desk or similar surface. Place your hands in front of your thighs pointing down, palms away from you. Keeping your elbows straight, start to press your fingers and palms onto the desk. Try to flatten your whole hand onto the surface.", Duration = ":30 hold." });

            return movementListMdl;
        }

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
