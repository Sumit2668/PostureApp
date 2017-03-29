using SQLite;
using System;

namespace PostureApp.Model
{
    public class ScheduleTbl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DayStarts { get; set; }
        public DateTime DayEnds { get; set; }
        public string MoveFreq { get; set; }
        public bool Sun { get; set; }
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public string AlertTone { get; set; }
    }
}