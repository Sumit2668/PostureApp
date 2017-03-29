using System;
using System.IO;
using PostureApp.Droid.SQLite;
using PostureApp.SQLite;
using SQLite;
using Xamarin.Forms;


[assembly: Dependency(typeof(SQLiteHelper))]
namespace PostureApp.Droid.SQLite
{
    public class SQLiteHelper: ISQLiteHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}