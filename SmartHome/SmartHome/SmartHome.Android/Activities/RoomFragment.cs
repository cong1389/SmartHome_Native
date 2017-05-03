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

namespace SmartHome.Droid.Activities
{
    public class RoomFragment : Fragment
    {
        public RoomFragment()
        {
            RetainInstance = true;
        }

        string houseId = string.Empty;
        public string HouseId
        {
            get { return houseId; }
        }

        //HouseResponseCollection houseResponseCollection;

        GridView grdHouse;

        //public static RoomFragment NewInstance(string _houseId)
        //{
        //    var roomFragment = new RoomFragment { Arguments = new Bundle() };
        //    //roomFragment.Arguments.PutString("houseId", houseId);
        //    return roomFragment;
        //}

        private async Task GetRoomData(string houseId, View view)
        {
            //HouseActivity objHouse = await APIManager.GetHouseByHouseId(houseId);
            //if (objHouse != null)
            //{
            //    List<Room> lstRoom = objHouse.rooms;
            //    var grdHouse = view.FindViewById<GridView>(Resource.Id.grdHouse);
            //    grdHouse.Adapter = new RoomAdapter(Activity, lstRoom);
            //}
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //View view = inflater.Inflate(Resource.Layout.Room, container, false);
            //GetRoomData(houseId, view);



            return view;

            //base.OnCreateView (inflater.Inflate(Resource.Layout.homeLayout, container, savedInstanceState);
        }
    }
}