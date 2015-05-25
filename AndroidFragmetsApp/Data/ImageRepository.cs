using System;
using SQLite;
using Android.Database.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace AndroidFragmetsApp
{
    public class ImageRepository
    {
        public ImageRepository()
        {
        }
        SQLiteConnection database;
        public ImageRepository(string filename)
        {
             database = new SQLiteDb().GetConnection(filename);
            database.CreateTable<Photo>();
        }

        public IEnumerable<Photo> GetItems()
        {
            return (from i in database.Table<Photo>() select i).ToList();
        }
        public Photo GetItem(int id)
        {
            return database.Table<Photo>().FirstOrDefault(x => x.Id == id);
        }
        public int SaveItem(Photo item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}

