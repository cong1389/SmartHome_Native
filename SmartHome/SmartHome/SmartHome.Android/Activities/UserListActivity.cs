using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using SmartHome.Service;
using SmartHome.Model;
using SmartHome.Droid.Common;
using Android.Support.Design.Widget;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "UserListActivity")]
    public class UserListActivity : AppCompatActivity
    {
        List<User> lstUser = null;

        protected override async void OnResume()
        {
            base.OnResume();

            SetContentView(Resource.Layout.UserListLayout);

            Title = "User Management";

            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            GetData();

            var btnAddUser = (FloatingActionButton)FindViewById(Resource.Id.btnAddUser);
            btnAddUser.Click += (sender, e) =>
            {
                StartActivity(new Intent(Application.Context, typeof(UserEditActivity)));
            };
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);  
        }
        private async Task GetData()
        {
            lstUser = await APIManager.GetUserAll();
            var userList_list = FindViewById<ListView>(Resource.Id.userList_list);
            userList_list.Adapter = new UserAdapter(this, lstUser);
            userList_list.ItemClick += UserList_grd_ItemClick;
        }
        private void UserList_grd_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string userId = lstUser[e.Position].userId;

            var userEditActivity = new Intent(this, typeof(UserEditActivity));
            userEditActivity.PutExtra("userId", userId);
            StartActivity(userEditActivity);
        }
    }
}