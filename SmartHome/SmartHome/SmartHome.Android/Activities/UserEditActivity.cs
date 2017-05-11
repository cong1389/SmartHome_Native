using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "UserEditActivity")]
    public class UserEditActivity : AppCompatActivity
    {
     //   View _MainLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserListLayout);
        }
    }
}