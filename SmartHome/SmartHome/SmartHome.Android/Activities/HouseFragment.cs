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
using SmartHome.Model;
using System.Threading.Tasks;
using SmartHome.Util;
using SmartHome.Service;
using SmartHome.Droid.Common;

namespace SmartHome.Droid.Activities
{
    public class HouseFragment : Fragment
    {
        List<House> lstHouse = null;

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
            View view = inflater.Inflate(Resource.Layout.House, container, false);
            GetHouseData(view);

            return view;//base.OnCreateView (inflater.Inflate(Resource.Layout.homeLayout, container, savedInstanceState);
        }

        private async Task GetHouseData(View view)
        {
            User user = AppInstance.user;          
            if (user.houses != null && user.houses.Count > 0)
            {
                List<House> lstHouse = await APIManager.GetHouseByHouseId(user.houses[0].houseId);
                //House objHouse = await APIManager.GetHouseByHouseId(lstHouse[0].houseId);
                //lstHouse.Clear();
                //lstHouse.Add(objHouse);

                ListView listViewHouse = view.FindViewById<ListView>(Resource.Id.listViewHouse);
                listViewHouse.Adapter = new HouseAdapter(Activity, lstHouse);
                listViewHouse.ItemClick += GrdHouse_ItemClick;
            }
        }

        private void GrdHouse_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string houseId = e.View.FindViewById<TextView>(Resource.Id.txtHouseId).Text;

            var roomActivity = new Intent(Application.Context, typeof(RoomActivity));
            roomActivity.PutExtra("houseId", houseId);           
            StartActivity(roomActivity);

            
            //RoomFragment fragment = new RoomFragment(houseId);
            //var ft = FragmentManager.BeginTransaction();
            //ft.Replace(Resource.Id.HomeFrameLayout, fragment);
            //ft.SetTransition(FragmentTransit.EnterMask);
            //ft.Commit();
        }
    }
}