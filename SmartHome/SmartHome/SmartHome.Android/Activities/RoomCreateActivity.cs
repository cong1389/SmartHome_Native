
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using System.Threading.Tasks;
using SmartHome.Util;
using SmartHome.Service.Response;
using SmartHome.Service;
using SmartHome.Model;
using System.Collections.Generic;
using SmartHome.Droid.Common;
using System;
using Android.Content;
using Android.Views;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "Smart Home - Create Room", ParentActivity = typeof(RoomActivity))]
    public class RoomCreateActivity : AppCompatActivity
    {
        #region Parameter

        string houseId = string.Empty;

        List<Room> lstRoom = null;

        GridView grdHouse;

        EditText RoomCreate_txtRoomName;

        #endregion

        #region Common

        #endregion

        #region Event

        protected override async void OnResume()
        {
            base.OnResume();

            houseId = Intent.GetStringExtra("houseId");

            RoomCreate_txtRoomName = FindViewById<EditText>(Resource.Id.RoomCreate_txtRoomName);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RoomCreateLayout);

            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            Title = "CREATE ROOM";
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.RoomBar, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.RoomBar_mnuCreateRoom).SetVisible(false);
                menu.FindItem(Resource.Id.RoomBar_mnuCreateDevice).SetVisible(false);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.RoomBar_mnuSave:
                    RoomCreate();
                    break;              
            }

            return base.OnOptionsItemSelected(item);
        }

        private void RoomCreate()
        {
            var alert = new Android.App.AlertDialog.Builder(this);

            if (string.IsNullOrWhiteSpace(RoomCreate_txtRoomName.Text) || string.IsNullOrWhiteSpace(RoomCreate_txtRoomName.Text))
            {
                alert.SetTitle("Invalid room name!");

                alert.SetMessage("An acquaintance must have both a first and last name.");

                alert.SetNegativeButton("OK", (senderAlert, args) =>
                {
                    // an empty delegate body, because we just want to close the dialog and not take any other action
                });

                //run the alert in UI thread to display in the screen
                RunOnUiThread(() =>
                {
                    alert.Show();
                });

                return;
            }
            string roomName = RoomCreate_txtRoomName.Text;

            APIManager.RoomCreate(houseId, roomName);

            //OnBackPressed();
        }

        public override void OnBackPressed()
        {
            if (FragmentManager.BackStackEntryCount != 0)
            {
                FragmentManager.PopBackStack();// fragmentManager.popBackStack();
            }
            else
            {
                base.OnBackPressed();
            }
        }

        #endregion

    }
}