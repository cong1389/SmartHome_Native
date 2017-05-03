
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
using Android.Support.V4.App;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "DeviceActivity", ParentActivity = typeof(RoomActivity))]
    public class DeviceActivity : AppCompatActivity
    {
        #region Parameter

        string roomId = string.Empty, houseId = string.Empty;

        #endregion

        #region Common

        private async Task GetDeviceData(string houseId, string roomId)
        {
            Room objRoom = await APIManager.GetDeviceByRoomId(houseId, roomId);
            if (objRoom != null)
            {
                List<Devices> lstDevice = objRoom.devices;
                var grdHouse = FindViewById<GridView>(Resource.Id.grdHouse);
                grdHouse.Adapter = new DeviceAdapter(this, lstDevice, houseId, roomId);

            }
        }

        #endregion

        #region Event

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                // Create your application here
                houseId = Intent.GetStringExtra("houseId") ?? "houseId not available";
                roomId = Intent.GetStringExtra("roomId") ?? "roomId not available";
                Title = Intent.GetStringExtra("roomName") ?? "roomName not available";

                SetContentView(Resource.Layout.Device);

                //// Init toolbar
                var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
                SetSupportActionBar(toolbar);                
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetDisplayShowHomeEnabled(true);

                await GetDeviceData(houseId, roomId);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }
        }

        #endregion

    }
}