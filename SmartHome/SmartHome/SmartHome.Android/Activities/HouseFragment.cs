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
            lstHouse = user.houses;
            if (lstHouse != null && lstHouse.Count > 0)
            {
                House objHouse = await APIManager.GetHouseByHouseId(lstHouse[0].houseId);
                lstHouse.Clear();
                lstHouse.Add(objHouse);

                var grdHouse = view.FindViewById<GridView>(Resource.Id.grdHouse);
                grdHouse.Adapter = new HouseAdapter(Activity, lstHouse);
                grdHouse.ItemClick += GrdHouse_ItemClick;
            }
        }

        private void GrdHouse_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            //var details = FragmentManager.FindFragmentById(Resource.Id.HomeFrameLayout) as HouseFragment;
            //if (details == null )
            //{

            // Make new fragment to show this selection.

            // Execute a transaction, replacing any existing
            // fragment with this one inside the frame.


            //}

            string houseId = e.View.FindViewById<TextView>(Resource.Id.txtHouseId).Text;
            //string houseName = lstHouse[e.Position].name;

            //var roomActivity = new Intent(Activity, typeof(RoomActivity));
            //roomActivity.PutExtra("houseId", houseId);
            //roomActivity.PutExtra("houseName", houseName);

            //StartActivity(roomActivity);

            RoomFragment fragment = new RoomFragment(houseId);            
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.HomeFrameLayout, fragment);
            ft.SetTransition(FragmentTransit.EnterMask);
            ft.Commit();
        }
    }
}