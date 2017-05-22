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
using SmartHome.Service;
using SmartHome.Service.Response;
using System.Threading.Tasks;

namespace SmartHome.Droid.Fragments
{
    public class RoomEditFragment : Android.App.DialogFragment
    {
        string houseId = string.Empty, roomId = string.Empty;

        public RoomEditFragment(string houseId, string roomId)
        {
            this.houseId = houseId;
            this.roomId = roomId;
        }

        public RoomEditFragment NewInstance(Bundle bundle)
        {
            RoomEditFragment fragment = new RoomEditFragment(null, null);
            fragment.Arguments = bundle;

            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.RoomEditLayout, container, false);

            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //remove title area
            //Dialog.SetCanceledOnTouchOutside(false); //dismiss window on touch outside

            Button btnScanClose = view.FindViewById<Button>(Resource.Id.btnScanClose);
            btnScanClose.Click += delegate
            {
                Dismiss();
                //Toast.MakeText(Activity, "Dialog fragment dismissed!", ToastLength.Short).Show();
            };

            //Update
            EditText RoomEdit_txtRoomName = view.FindViewById<EditText>(Resource.Id.RoomEdit_txtRoomName);
            Button RoomEdit_btnScanIP = view.FindViewById<Button>(Resource.Id.RoomEdit_btnScanIP);
            if (RoomEdit_btnScanIP.Text.Length > 0)
            {
                RoomEdit_btnScanIP.Click += async delegate
                {
                    string msg = string.Empty;

                    StatusResponse response = await APIManager.RoomUpdate(houseId, roomId, RoomEdit_txtRoomName.Text);
                    if (response != null)
                    {
                        if (response.Success)
                            msg = "Save success.";
                        else
                            msg = "Save fail.";
                    }

                    Dismiss();
                    Toast.MakeText(Activity, msg, ToastLength.Short).Show();
                };
            }           

            return view;
        }
    }
}