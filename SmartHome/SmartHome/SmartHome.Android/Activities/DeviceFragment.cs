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
    public class DeviceFragment : Fragment
    {
        List<Devices> lstDevice = null;

        string houseId = string.Empty,roomId=string.Empty;       

        public DeviceFragment()
        {
            RetainInstance = true;
        }

        public DeviceFragment(string houseId, string roomId)
        {
            this.houseId = houseId;
            this.roomId = roomId;
        }  

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            
            View view = inflater.Inflate(Resource.Layout.Device, container, false);
            GetDeviceData(view);

            return view;
        }

        private async Task GetDeviceData(View view)
        {
            Room objRoom = await APIManager.GetDeviceByRoomId(houseId, roomId);
            if (objRoom != null)
            {
                List<Devices> lstDevice = objRoom.devices;
                var grdHouse = view.FindViewById<GridView>(Resource.Id.grdHouse);
                grdHouse.Adapter = new DeviceAdapter(Activity, lstDevice, houseId, roomId);
            }
        }
    }
}