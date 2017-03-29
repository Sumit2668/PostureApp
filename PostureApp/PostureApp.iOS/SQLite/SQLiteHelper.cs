using System;
using System.IO;
using PostureApp.iOS.SQLite;
using PostureApp.SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteHelper))]
namespace PostureApp.iOS.SQLite
{
    public class SQLiteHelper:ISQLiteHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}