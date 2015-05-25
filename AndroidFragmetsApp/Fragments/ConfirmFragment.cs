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
using Android.Graphics;

namespace AndroidFragmetsApp
{
    public class ConfirmFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Confirm, container, false);
            var takePhotoBtn = view.FindViewById<Button>(Resource.Id.takePhotoBtn);
            var doneBtn = view.FindViewById<Button>(Resource.Id.doneBtn);
            var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
            imageView.SetImageURI(Android.Net.Uri.Parse(App._file.Path));
            takePhotoBtn.Click += TakeAPicture;
            doneBtn.Click += SaveAPicture;
            return view;
        }

        private async void SaveAPicture(object sender, EventArgs eventArgs)
        {
            var commentText = Activity.FindViewById<EditText>(Resource.Id.commentText);
            var image = Activity.FindViewById<ImageView>(Resource.Id.imageView);
            var photo = new Photo{ Comment = commentText.Text, ImagePath = App._file.Path };
            var fragmentContainer = FragmentManager.FindFragmentById(Resource.Id.fragment_container);
            var api = new RestApi();
            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();  
            App.repo.SaveItem(photo);
            await api.SaveImageData(photo);

            if (fragmentContainer is ConfirmFragment)
            {
                fragmentTx.Replace(Resource.Id.fragment_container, new GalleryFragment());
            }
            else
            {
                fragmentTx.Replace(Resource.Id.fragment_navigation, new GalleryFragment());
            }
            fragmentTx.Commit();

        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            App._file.Delete();
            Intent intent = new Intent(MediaStore.ActionImageCapture);      
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            Activity.SendBroadcast(mediaScanIntent);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = Resources.DisplayMetrics.WidthPixels;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            var imageView = Activity.FindViewById<ImageView>(Resource.Id.imageView);
            imageView.SetImageURI(Android.Net.Uri.Parse(App._file.Path));
        }

    }
}

