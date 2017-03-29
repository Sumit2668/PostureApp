using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PostureApp.Model
{
    public class MovementListTbl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string MoveTitle { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public bool Selected { get; set; }
        public string MoveIcon { get; set; }
    }
}
