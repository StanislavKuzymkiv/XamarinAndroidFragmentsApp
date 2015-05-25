using System;

namespace AndroidFragmetsApp
{
    public class SQLiteDb
    {
        public SQLiteDb()
        {
        }
        public SQLite.SQLiteConnection GetConnection (string filename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentsPath, filename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }

    }
}

