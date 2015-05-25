using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace AndroidFragmetsApp
{
    public class GalleryFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Gallery, container, false);
            var list = view.FindViewById<ListView>(Resource.Id.listView);
            list.Adapter = new CusotmListAdapter(Activity, App.repo.GetItems().ToList());
            return view;
        }

    }
}

