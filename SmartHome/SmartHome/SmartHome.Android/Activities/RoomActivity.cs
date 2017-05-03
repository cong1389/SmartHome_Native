
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

        //private async Task GetRoomData(string houseId)
        //{
        //    House objHouse = await APIManager.GetHouseByHouseId(houseId);
          
        //    if (objHouse != null)
        //    {
        //        lstRoom = objHouse.rooms;
        //        var grdHouse = FindViewById<GridView>(Resource.Id.grdHouse);
        //        grdHouse.Adapter = new RoomAdapter(this, lstRoom);
        //        grdHouse.ItemClick += GrdHouse_ItemClick;
        //    }
        //}

        #endregion

        #region Event

        protected override async void OnResume()
        {
            base.OnResume();

            // Create your application here
            SetContentView(Resource.Layout.Room);
            
            //// Init toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);            

            //Lấy obj house đã lưu trước đó
            House objHouse_Result = AppInstance.houseData;

            //Nếu house trước đó ==NULL, thì gọi lại API GetHouse
            if (objHouse_Result == null)
            {
                houseId = Intent.GetStringExtra("houseId") ?? "houseId not available";                

                objHouse_Result = await APIManager.GetHouseByHouseId(houseId);
            }
            else
            {
                lstRoom = objHouse_Result.rooms;               
            }

            if (objHouse_Result !=null)
            {
                houseId = objHouse_Result.houseId;
                Title = objHouse_Result.name ?? "houseName not available";

                var grdHouse = FindViewById<GridView>(Resource.Id.grdHouse);
                grdHouse.Adapter = new RoomAdapter(this, lstRoom);
                grdHouse.ItemClick += GrdHouse_ItemClick;
            }
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        private void GrdHouse_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string roomId = lstRoom[e.Position].roomId;
            string roomName = lstRoom[e.Position].name;

            var deviceActivity = new Intent(this, typeof(DeviceActivity));
            deviceActivity.PutExtra("houseId", houseId);
            deviceActivity.PutExtra("roomId", roomId);
            deviceActivity.PutExtra("roomName", roomName);
            StartActivity(deviceActivity);
        }

        #endregion

    }
}