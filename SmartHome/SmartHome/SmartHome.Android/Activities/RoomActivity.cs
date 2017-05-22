
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
    [Activity(Label = "Smart Home - Room", ParentActivity = typeof(HomeActivity))]
    public class RoomActivity : AppCompatActivity
    {
        #region Parameter

        string houseId = string.Empty;

        List<Room> lstRoom = null;

        GridView grdHouse;

        #endregion

        #region Common
        private async Task GetRoomData()
        {
            //Lấy obj house đã lưu trước đó
            House objHouse_Result = AppInstance.houseData;

            //Nếu house trước đó == NULL, thì gọi lại API GetHouse
            if (objHouse_Result == null)
            {
                List<House> lstHouse = await APIManager.GetHouseByHouseId(houseId);
                objHouse_Result = lstHouse[0];
                lstRoom = objHouse_Result.rooms;
            }
            else
            {
                lstRoom = objHouse_Result.rooms;
            }

            if (objHouse_Result != null)
            {
                houseId = objHouse_Result.houseId;

                //view.Title = objHouse_Result.name ?? "houseName not available";

                ListView listViewRoom = FindViewById<ListView>(Resource.Id.listViewRoom);
                listViewRoom.Adapter = new RoomAdapter(this, lstRoom,houseId);
                listViewRoom.ItemClick += GrdHouse_ItemClick;
            }
        }

        #endregion

        #region Event

        protected override async void OnResume()
        {
            base.OnResume();

            houseId = Intent.GetStringExtra("houseId");

            // Create your application here
            SetContentView(Resource.Layout.Room);

            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            TextView room_txtUserName = FindViewById<TextView>(Resource.Id.room_txtUserName);
            room_txtUserName.Text = AppInstance.user.username.ToUpper();

            await GetRoomData();
        }
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.RoomBar, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.RoomBar_mnuSave).SetVisible(false);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.RoomBar_mnuCreateRoom:
                    var roomCreateActivity = new Intent(this, typeof(RoomCreateActivity));
                    roomCreateActivity.PutExtra("houseId", houseId);
                    StartActivity(roomCreateActivity);
                    break;
                case Resource.Id.RoomBar_mnuCreateDevice:
                    var deviceCreateActivity = new Intent(this, typeof(DeviceCreateActivity));
                    deviceCreateActivity.PutExtra("houseId", houseId);
                    StartActivity(deviceCreateActivity);
                    break;

            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
              
        private void GrdHouse_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string roomId = lstRoom[e.Position].roomId;
            string roomName = lstRoom[e.Position].name;

            //var deviceActivity = new Intent(this, typeof(DeviceActivity));
            //deviceActivity.PutExtra("houseId", houseId);
            //deviceActivity.PutExtra("roomId", roomId);
            //deviceActivity.PutExtra("roomName", roomName);
            //StartActivity(deviceActivity);
        }

        #endregion

    }
}