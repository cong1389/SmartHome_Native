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

namespace SmartHome.Droid.Fragments
{
    public class ScanIPFragment : Android.App.DialogFragment
    {
        //public override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    // Create your fragment here
        //}

        public static ScanIPFragment NewInstance(Bundle bundle)
        {
            ScanIPFragment fragment = new ScanIPFragment();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.ScanIP, container, false);
            Button button = view.FindViewById<Button>(Resource.Id.btnScanIP);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //remove title area
            Dialog.SetCanceledOnTouchOutside(false); //dismiss window on touch outside

            button.Click += delegate {
                Dismiss();
                Toast.MakeText(Activity, "Dialog fragment dismissed!", ToastLength.Short).Show();
            };

            return view;
        }
    }
}