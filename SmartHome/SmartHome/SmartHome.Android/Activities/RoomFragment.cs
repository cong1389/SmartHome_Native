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
using SmartHome.Service.Response;
using System.Threading.Tasks;
using SmartHome.Model;
using SmartHome.Service;
using SmartHome.Droid.Common;
using SmartHome.Util;

namespace SmartHome.Droid.Activities
{
    public class RoomFragment : Fragment
    {
        List<Room> lstRoom = null;

        string houseId = string.Empty;
        public string HouseId
        {
            get { return houseId; }
        }

        public RoomFragment()
        {
            RetainInstance = true;
        }

        public RoomFragment(string houseId)
        {
            this.houseId = houseId;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.Room, container, false);
            GetRoomData(view);

            return view;
        }

        private async Task GetRoomData(View view)
        {
            //// Init toolbar
            //var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            //SetSupportActionBar(toolbar);
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);

            //Lấy obj house đã lưu trước đó
            House objHouse_Result = AppInstance.houseData;

            //Nếu house trước đó == NULL, thì gọi lại API GetHouse
            if (objHouse_Result == null)
            {
                objHouse_Result = await APIManager.GetHouseByHouseId(houseId);
            }
            else
            {
                lstRoom = objHouse_Result.rooms;
            }

            if (objHouse_Result != null)
            {
                houseId = objHouse_Result.houseId;
               
                //view.Title = objHouse_Result.name ?? "houseName not available";

                var grdHouse = view.FindViewById<GridView>(Resource.Id.grdHouse);
                grdHouse.Adapter = new RoomAdapter(Activity, lstRoom);
                grdHouse.ItemClick += GrdHouse_ItemClick;
            }
        }

        private void GrdHouse_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string roomId = lstRoom[e.Position].roomId;
            string roomName = lstRoom[e.Position].name;

            DeviceFragment fragment = new DeviceFragment(houseId, roomId);
            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack("fdsa");
            ft.Replace(Resource.Id.HomeFrameLayout, fragment);
            ft.SetTransition(FragmentTransit.EnterMask);
            ft.Commit();

            //var deviceActivity = new Intent(this, typeof(DeviceActivity));
            //deviceActivity.PutExtra("houseId", houseId);
            //deviceActivity.PutExtra("roomId", roomId);
            //deviceActivity.PutExtra("roomName", roomName);
            //StartActivity(deviceActivity);
        }
    }
}