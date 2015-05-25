using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;
using Android.Graphics;
using Android.Provider;
using Android.Util;

namespace AndroidFragmetsApp
{
    [Activity(Label = "AndroidFragmetsApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();   
            App.repo = new ImageRepository("photos.db");
            fragmentTx.Add(Resource.Id.fragment_navigation, new NavigationFragment());
            fragmentTx.DisallowAddToBackStack().Commit();
        }

        public override void OnBackPressed()
        {            
            var fragment = FragmentManager.FindFragmentById(Resource.Id.fragment_navigation);
            if (fragment is GalleryFragment)
            {
                FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();  
                fragmentTx.Replace(Resource.Id.fragment_navigation, new NavigationFragment());
                fragmentTx.DisallowAddToBackStack().Commit();
                return;
            }
            base.OnBackPressed();
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
        }
    }

}


