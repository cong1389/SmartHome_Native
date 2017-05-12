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
        View view;

        public UserListFragment()
        {
            RetainInstance = true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();

        //    GetData(view);
        //}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            view = inflater.Inflate(Resource.Layout.UserListLayout, container, false);
            GetData(view);

            var btnAddUser = (FloatingActionButton)view.FindViewById(Resource.Id.btnAddUser);
            btnAddUser.Click += (sender, e) =>
            {
                StartActivity(new Intent(Application.Context, typeof(UserEditActivity)));
            };

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
            User obj_Result = AppInstance.user;
            List<User> lstUser = await APIManager.GetUserAll();

            ////Nếu house trước đó == NULL, thì gọi lại API GetHouse
            //if (obj_Result == null)
            //{
            //    userResponseCollection = await APIManager.GetUserAll();
            //    lstUser = (List<User>)userResponseCollection.data;
            //}
            //else
            //{
            //    lstUser = obj_Result;
            //}

            //if (obj_Result != null)
            //{
            //    //houseId = obj_Result.houseId;

            //    //view.Title = objHouse_Result.name ?? "houseName not available";

            var userList_grd = view.FindViewById<GridView>(Resource.Id.userList_grd);
            userList_grd.Adapter = new UserAdapter(Activity, lstUser);
            userList_grd.ItemClick += UserList_grd_ItemClick; ;
        }

        private void UserList_grd_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string userId = lstUser[e.Position].userId;

            var userEditActivity = new Intent(Activity, typeof(UserEditActivity));
            userEditActivity.PutExtra("userId", userId);
            StartActivity(new Intent(Activity, typeof(UserEditActivity)));
        }
    }
}