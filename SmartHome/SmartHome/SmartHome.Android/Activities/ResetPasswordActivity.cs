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
using SmartHome.Model;
using SmartHome.Util;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "ResetPasswordActivity")]
    public class ResetPasswordActivity : Activity
    {
        #region Parameter

        #endregion

        #region Common

        #endregion

        #region Event

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ResetPasswordLayout);

            User objUser = AppInstance.user;
            //objUser.roles
        }

        #endregion


    }
}