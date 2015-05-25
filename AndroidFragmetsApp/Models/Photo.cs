using System;
using SQLite;

namespace AndroidFragmetsApp
{
    [Table("Photos")]
    public class Photo
    {
        public Photo ()
        {
        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Comment {get;set;}
    }
}


