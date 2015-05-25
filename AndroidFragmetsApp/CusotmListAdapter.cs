using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Views;

namespace AndroidFragmetsApp
{
    public class CusotmListAdapter:BaseAdapter<Photo>
    {
        public CusotmListAdapter()
        {
        }

        Activity context;
        List<Photo> list;

        public CusotmListAdapter(Activity _context, List<Photo> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Photo this [int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; 
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItem, parent, false);

            Photo item = this[position];
            view.FindViewById<ImageView>(Resource.Id.imageViewItem).SetImageURI(Android.Net.Uri.Parse(item.ImagePath));
            view.FindViewById<TextView>(Resource.Id.CommentItem).Text = item.Comment;

           
            return view;
        }
    }
}

