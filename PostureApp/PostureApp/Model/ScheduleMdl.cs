using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SQLite.Net.Attributes;
using Xamarin.Forms;

namespace PostureApp.Model
{
    public class ScheduleMdl
    {
        private string _sunTxtColor;

        public int Id { get; set; }
        public DateTime DayStarts { get; set; }
        public DateTime DayEnds { get; set; }
        public string MoveFreq { get; set; }
        public double MoveFreq_Int { get; set; }
        public bool Sun { get; set; }
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public string AlertTone { get; set; }

        public Color sunTxtColor { get; set; }
        public Color monTxtColor { get; set; }
        public Color tueTxtColor { get; set; }
        public Color wedTxtColor { get; set; }
        public Color thuTxtColor { get; set; }
        public Color friTxtColor { get; set; }
        public Color satTxtColor { get; set; }

        public Color sunBgColor { get; set; }
        public Color monBgColor { get; set; }
        public Color tueBgColor { get; set; }
        public Color wedBgColor { get; set; }
        public Color thuBgColor { get; set; }
        public Color friBgColor { get; set; }
        public Color satBgColor { get; set; }

        public static ScheduleMdl PrepareList(ScheduleTbl lst)
        {

            ScheduleMdl scheduleMdl = new ScheduleMdl();

            scheduleMdl.Id = lst.Id;
            scheduleMdl.DayStarts = lst.DayStarts;
            scheduleMdl.DayEnds = lst.DayEnds;
            scheduleMdl.MoveFreq = lst.MoveFreq;
            scheduleMdl.Sun = lst.Sun;
            scheduleMdl.Mon = lst.Mon;
            scheduleMdl.Tue = lst.Tue;
            scheduleMdl.Wed = lst.Wed;
            scheduleMdl.Thu = lst.Thu;
            scheduleMdl.Fri = lst.Fri;
            scheduleMdl.Sat = lst.Sat;
            scheduleMdl.AlertTone = lst.AlertTone;


			if (scheduleMdl.Sun==true)
            {
				scheduleMdl.sunBgColor = Color.FromHex("#FFFFFF");
				scheduleMdl.sunTxtColor = Color.FromHex("#00858f");
            }                  
            else               
            {
				scheduleMdl.sunBgColor = Color.FromHex("#00858f");
				scheduleMdl.sunTxtColor = Color.FromHex("#FFFFFF");
            }

            if (scheduleMdl.Mon == true)
            {
				scheduleMdl.monBgColor = Color.FromHex("#FFFFFF");
				scheduleMdl.monTxtColor = Color.FromHex("#00858f");
            }
            else
            {
				scheduleMdl.monBgColor = Color.FromHex("#00858f");
				scheduleMdl.monTxtColor = Color.FromHex("#FFFFFF");
            }
            
            if (scheduleMdl.Tue==true)
            {
				scheduleMdl.tueBgColor = Color.FromHex("#FFFFFF");
				scheduleMdl.tueTxtColor = Color.FromHex("#00858f");
            }
            else
            {
				scheduleMdl.tueBgColor = Color.FromHex("#00858f");
				scheduleMdl.tueTxtColor = Color.FromHex("#FFFFFF");
                
            }
            if (scheduleMdl.Wed==true)
            {
               scheduleMdl.wedBgColor = Color.FromHex("#FFFFFF");
				scheduleMdl.wedTxtColor = Color.FromHex("#00858f");
            }
            else
            {
				scheduleMdl.wedBgColor = Color.FromHex("#00858f");
				scheduleMdl.wedTxtColor = Color.FromHex("#FFFFFF");
                
            }
            if (scheduleMdl.Thu==true)
            {
                
                scheduleMdl.thuBgColor = Color.FromHex("#FFFFFF");
				scheduleMdl.thuTxtColor = Color.FromHex("#00858f");
            }
            else
            {
                scheduleMdl.thuBgColor = Color.FromHex("#00858f");
				scheduleMdl.thuTxtColor = Color.FromHex("#FFFFFF");
			}
            if(scheduleMdl.Fri==true)
            {
                 scheduleMdl.friBgColor = Color.FromHex("#FFFFFF");
				scheduleMdl.friTxtColor = Color.FromHex("#00858f");
            }
            else
            {scheduleMdl.friBgColor = Color.FromHex("#00858f");
				scheduleMdl.friTxtColor = Color.FromHex("#FFFFFF");
               
            }
            if (scheduleMdl.Sat==true)
            {
                
				 scheduleMdl.satBgColor = Color.FromHex("#FFFFFF");
				scheduleMdl.satTxtColor = Color.FromHex("#00858f");
            }
            else
            {
               scheduleMdl.satBgColor = Color.FromHex("#00858f");
				scheduleMdl.satTxtColor = Color.FromHex("#FFFFFF");
            }
        return scheduleMdl;

        }

        public double GetFrequency()
        {
            double freq = 15;

            if (MoveFreq  == "15 Min")
            {
                freq = 15 ;
            }
            else if (MoveFreq == "30 Min")
            {
                freq = 30;
            }
            else if (MoveFreq == "45 Min")
            {
                freq = 45;
            }
            else if (MoveFreq=="hour")
            {
                freq = 60;
            }
            else if (MoveFreq=="90 Min")
            {
                freq = 90;
            }
            else if (MoveFreq== "2 hour")
            {
                
                freq = 120;
            }
            else if (MoveFreq=="3 hour")
            {
                
                freq = 180;
            }
            else if (MoveFreq== "4 hour")
            {
                freq = 240;
            }
            return freq;
        }

    }
}