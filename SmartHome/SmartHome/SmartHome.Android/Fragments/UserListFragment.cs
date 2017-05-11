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
using SmartHome.Service.Response;
using Android.Support.Design.Widget;
using SmartHome.Droid.Activities;

namespace SmartHome.Droid.Fragments
{
    public class UserListFragment : Fragment
    {
        List<User> lstUser = null;

        public UserListFragment()
        {
            RetainInstance = true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.UserListLayout, container, false);
            GetData(view);

            //var addButton = (FloatingActionButton)view.FindViewById(Resource.Id.btnAddUser);

            //addButton.Click += (sender, e) =>
            //{
            //    StartActivity(new Intent(Application.Context, typeof(UserEditActivity)));
            //};

            return view;
        }

        private async Task GetData(View view)
        {
            //// Init toolbar
            //var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            //SetSupportActionBar(toolbar);
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);

            //Lấy obj house đã lưu trước đó
            //User obj_Result = AppInstance.user;
            List<User> lstUser = await APIManager.GetUserAll();


            //Nếu house trước đó == NULL, thì gọi lại API GetHouse
            //if (obj_Result == null)
            //{
            //    userResponseCollection = await APIManager.GetUserAll();
            //    lstUser = (List<User>)userResponseCollection.data;
            //}
            ////else
            ////{
            ////    lstUser = obj_Result;
            ////}

            //if (obj_Result != null)
            //{
            //    //houseId = obj_Result.houseId;

            //    //view.Title = objHouse_Result.name ?? "houseName not available";

            var grdHouse = view.FindViewById<GridView>(Resource.Id.grdHouse);
            grdHouse.Adapter = new UserAdapter(Activity, lstUser);
            //    //grdHouse.ItemClick += GrdHouse_ItemClick;
            //}
        }

        //private void GrdHouse_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    string roomId = lstUser[e.Position].roomId;
        //    string roomName = lstUser[e.Position].name;

        //    DeviceFragment fragment = new DeviceFragment(houseId, roomId);
        //    var ft = FragmentManager.BeginTransaction();
        //    ft.AddToBackStack("fdsa");
        //    ft.Replace(Resource.Id.HomeFrameLayout, fragment);
        //    ft.SetTransition(FragmentTransit.EnterMask);
        //    ft.Commit();

        //    //var deviceActivity = new Intent(this, typeof(DeviceActivity));
        //    //deviceActivity.PutExtra("houseId", houseId);
        //    //deviceActivity.PutExtra("roomId", roomId);
        //    //deviceActivity.PutExtra("roomName", roomName);
        //    //StartActivity(deviceActivity);
        //}
    }
}