using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using System.Threading.Tasks;
using SmartHome.Util;
using SmartHome.Service.Response;
using SmartHome.Service;
using SmartHome.Model;
using System.Collections.Generic;
using SmartHome.Droid.Common;
using Android.Content;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using SmartHome.Droid.Fragments;
using System;
using System.Collections;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "UserEditActivity")]
    public class UserEditActivity : AppCompatActivity
    {
        #region Parameter

        View _MainLayout;
        View _ContentLayout;

        User objUser;
        string userId = string.Empty;

        EditText userEdit_Name;
        EditText userEdit_DeviceId;
        EditText userEdit_TenantId;
        EditText userEdit_UserName;
        EditText userEdit_PassWord;
        EditText userEdit_Mobile;
        EditText userEdit_Email;
        EditText userEdit_Address;

        Switch userEdit_switActive;
        AutoCompleteTextView userEdit_cboHouse;

        #endregion

        #region Common

        private async void GetData()
        {
            objUser = await APIManager.GetUserByUserId(userId);

            if (objUser != null)
            {
                userEdit_Name.Text = objUser.name;
                userEdit_DeviceId.Text = objUser.deviceId;
                userEdit_TenantId.Text = objUser.tenantId;
                userEdit_UserName.Text = objUser.username;
                userEdit_PassWord.Text = objUser.password;
                userEdit_Mobile.Text = objUser.mobile;
                userEdit_Email.Text = objUser.email;
                userEdit_Address.Text = objUser.address;

                //switch active/deactive user
                userEdit_switActive.Checked = objUser.active;
                userEdit_switActive.Tag = objUser.userId;
                userEdit_switActive.CheckedChange += UserEdit_switActive_CheckedChange;

                // Get all house bind vào adapter           
                List<House> lstHouse = await APIManager.GetHouseAll();
                List<House> lstHouseOld = objUser.houses;
                var userEdit_grdHouse = FindViewById<GridView>(Resource.Id.userEdit_grdHouse);
                userEdit_grdHouse.Adapter = new HouseItemAdapter(this, lstHouse,userId, lstHouseOld);

                ArrayList arrHouse = new ArrayList();
                foreach (House item in lstHouse)
                {
                    arrHouse.Add(item.name);
                }
                
                Spinner userEdit_spinHouse = FindViewById<Spinner>(Resource.Id.userEdit_spinHouse);                

                ArrayAdapter dynamicAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, arrHouse);                
                dynamicAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemChecked);                
                userEdit_spinHouse.Adapter = dynamicAdapter;
            }
        }

        private void Save()
        {
            //set alert for executing the task
            var alert = new Android.App.AlertDialog.Builder(this);

            if (string.IsNullOrWhiteSpace(userEdit_UserName.Text) || string.IsNullOrWhiteSpace(userEdit_UserName.Text))
            {
                alert.SetTitle("Invalid User Name!");

                alert.SetMessage("An acquaintance must have both a first and last name.");

                alert.SetNegativeButton("OK", (senderAlert, args) =>
                {
                    // an empty delegate body, because we just want to close the dialog and not take any other action
                });

                //run the alert in UI thread to display in the screen
                RunOnUiThread(() =>
                {
                    alert.Show();
                });

                return;
            }
            objUser = new User();
            objUser.name = userEdit_Name.Text;  objUser.deviceId = userEdit_DeviceId.Text;
            objUser.tenantId = userEdit_TenantId.Text;
            objUser.username = userEdit_UserName.Text;
            objUser.password = userEdit_PassWord.Text;
            objUser.mobile = userEdit_Mobile.Text;
            objUser.email = userEdit_Email.Text;
            objUser.address = userEdit_Address.Text;          

            if (userId == null)
            {
                APIManager.UserCreate(objUser);
            }
            else
            {
                objUser.userId = this.userId;
                objUser.active = userEdit_switActive.Checked;
                APIManager.UserUpdate(objUser);
            }

            //Task<StatusResponse> statusResponse= userId == string.Empty ? APIManager.UserCreate(objUser) : APIManager.UserUpdate(objUser);            

            OnBackPressed();
        }

        private void Delete()
        {
            APIManager.UserDelete(userId);
            OnBackPressed();
        }

        #endregion

        #region Event        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserEditLayout);

            //// Init toolbar
            var userEditTopMenu = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(userEditTopMenu);

            // ensure that the system bar color gets drawn
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            // enable the back button in the action bar
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetHomeButtonEnabled(true);

            //Title = SupportActionBar.Title = "";
        }

        protected override async void OnResume()
        {
            base.OnResume();

            userId = Intent.GetStringExtra("userId");

            userEdit_Name = FindViewById<EditText>(Resource.Id.userEdit_Name);
            userEdit_DeviceId = FindViewById<EditText>(Resource.Id.userEdit_DeviceId);
            userEdit_TenantId = FindViewById<EditText>(Resource.Id.userEdit_TenantId);
            userEdit_UserName = FindViewById<EditText>(Resource.Id.userEdit_UserName);
            userEdit_PassWord = FindViewById<EditText>(Resource.Id.userEdit_PassWord);
            userEdit_Mobile = FindViewById<EditText>(Resource.Id.userEdit_Mobile);
            userEdit_Email = FindViewById<EditText>(Resource.Id.userEdit_Email);
            userEdit_Address = FindViewById<EditText>(Resource.Id.userEdit_Address);

            userEdit_switActive = FindViewById<Switch>(Resource.Id.userEdit_switActive);
            //userEdit_cboHouse = FindViewById<AutoCompleteTextView>(Resource.Id.userEdit_cboHouse);

            GetData();

            
        }

        private async void UserEdit_switActive_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Switch switch1 = (Switch)sender;
            string id = (string)switch1.Tag;

            await APIManager.IsUserSetActive(id, e.IsChecked);
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.UserEditMenu, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.userEditMenuSaveButton).SetVisible(true);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                case Resource.Id.userEditMenuSaveButton:
                    Save();
                    break;
                case Resource.Id.userEdit_mnuDelete:
                    Delete();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

       

        #endregion
    }

}