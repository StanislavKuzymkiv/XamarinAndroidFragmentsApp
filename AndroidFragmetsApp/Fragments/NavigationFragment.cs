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
using Android.Provider;
using Java.IO;

namespace AndroidFragmetsApp
{
    public class NavigationFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Navigation, container, false);
            Button launchCamBtn = view.FindViewById<Button>(Resource.Id.launchCamBtn);
            Button galleryBtn = view.FindViewById<Button>(Resource.Id.galleryBtn);

            launchCamBtn.Click += TakeAPicture;        
            galleryBtn.Click += OpenGallery;
            return view;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            CreateDirectoryForPictures();
            Intent intent = new Intent(MediaStore.ActionImageCapture);      
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        private void OpenGallery(object sender, EventArgs eventArgs)
        {
            var fragmentContainer = Activity.FindViewById<LinearLayout>(Resource.Id.fragment_container);
            var navLayout = Activity.FindViewById<LinearLayout>(Resource.Id.fragment_navigation);
            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();  
            if (fragmentContainer != null)
            {
                navLayout.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent, 1f);
                fragmentTx.Replace(Resource.Id.fragment_container, new GalleryFragment());
            }
            else
            {
                 fragmentTx.Replace(Resource.Id.fragment_navigation, new GalleryFragment());
            }
            fragmentTx.Commit();

        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            var navLayout = Activity.FindViewById(Resource.Id.fragment_navigation);
            var fragmentContainer = Activity.FindViewById(Resource.Id.fragment_container);
            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();  
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            Activity.SendBroadcast(mediaScanIntent);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = Resources.DisplayMetrics.WidthPixels;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (fragmentContainer == null)
            {
                fragmentTx.Replace(Resource.Id.fragment_navigation, new ConfirmFragment());
            }
            else
            {
                navLayout.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent, 1f);

                if (FragmentManager.FindFragmentById(Resource.Id.fragment_container) is GalleryFragment)
                {                   
                    fragmentTx.Replace(Resource.Id.fragment_container, new ConfirmFragment());
                }
                else
                {
                    fragmentTx.Add(Resource.Id.fragment_container, new ConfirmFragment());
                }
            }
            fragmentTx.Commit();

        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CameraApp");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();

            }
        }
    }
}

