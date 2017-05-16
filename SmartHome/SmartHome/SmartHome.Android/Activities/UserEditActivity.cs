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

        User objUserCurrent;
        string userIdCurrent = string.Empty;

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

        LinearLayout userEdit_linearResetPassword;
        LinearLayout userEdit_linearChangePassword;
        TextView userEdit_txtResetPassword;
        TextView userEdit_txtChangePassword;

        #endregion

        #region Common

        private async void GetData()
        {
            objUserCurrent = await APIManager.GetUserByUserId(userIdCurrent);

            if (objUserCurrent != null)
            {
                userEdit_Name.Text = objUserCurrent.name;
                userEdit_DeviceId.Text = objUserCurrent.deviceId;
                userEdit_TenantId.Text = objUserCurrent.tenantId;
                userEdit_UserName.Text = objUserCurrent.username;
                userEdit_PassWord.Text = objUserCurrent.password;
                userEdit_Mobile.Text = objUserCurrent.mobile;
                userEdit_Email.Text = objUserCurrent.email;
                userEdit_Address.Text = objUserCurrent.address;

                //switch active/deactive user
                userEdit_switActive.Checked = objUserCurrent.active;
                userEdit_switActive.Tag = objUserCurrent.userId;
                userEdit_switActive.CheckedChange += UserEdit_switActive_CheckedChange;
                userEdit_switActive.Text = "Active/Deactive";

                // Get all house bind vào adapter           
                List<House> lstHouse = await APIManager.GetHouseAll();
                List<House> lstHouseOld = objUserCurrent.houses;
                var userEdit_grdHouse = FindViewById<GridView>(Resource.Id.userEdit_grdHouse);
                userEdit_grdHouse.Adapter = new HouseItemAdapter(this, lstHouse, userIdCurrent, lstHouseOld);

                ArrayList arrHouse = new ArrayList();
                foreach (House item in lstHouse)
                {
                    arrHouse.Add(item.name);
                }

                Spinner userEdit_spinHouse = FindViewById<Spinner>(Resource.Id.userEdit_spinHouse);

                string[] data = { "admin", "user" };
                ArrayAdapter dynamicAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, data);
                userEdit_spinHouse.Adapter = dynamicAdapter;
                userEdit_spinHouse.ItemSelected += UserEdit_spinHouse_ItemSelected;
                List<object> lstRoleCurrent = objUserCurrent.roles;
                string roleCurrent = lstRoleCurrent != null ? lstRoleCurrent[0].ToString() : string.Empty;
                userEdit_spinHouse.SetSelection(roleCurrent == "admin" ? 0 : 1); ;


                User objUser = AppInstance.user;
                List<object> lstRole = objUser != null ? objUser.roles : null;
                string roleAdmin = lstRole != null ? lstRole[0].ToString() : string.Empty;

                switch (roleAdmin)
                {
                    case "admin":
                        userEdit_linearChangePassword.Visibility = ViewStates.Invisible;
                        break;
                    case "user":
                        userEdit_linearResetPassword.Visibility = ViewStates.Invisible;
                        break;
                }
                //ArrayAdapter dynamicAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, arrHouse);
                //dynamicAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemChecked);
                //userEdit_spinHouse.Adapter = dynamicAdapter;
            }
        }

        private async void UserEdit_spinHouse_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string roleName = spinner.GetItemAtPosition(e.Position).ToString();
            User objUser = AppInstance.user;
            List<object> objRole = objUser.roles;
            string roleCurrent = objRole != null ? objRole[0].ToString() : string.Empty;

            if (roleCurrent == roleName)
            {
                await APIManager.UserAddDeleteRole(userIdCurrent, "admin", true);
            }
            else
            {
                await APIManager.UserAddDeleteRole(userIdCurrent, "admin", false);
            }

            //string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
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
            objUserCurrent = new User();
            objUserCurrent.name = userEdit_Name.Text; objUserCurrent.deviceId = userEdit_DeviceId.Text;
            objUserCurrent.tenantId = userEdit_TenantId.Text;
            objUserCurrent.username = userEdit_UserName.Text;
            objUserCurrent.password = userEdit_PassWord.Text;
            objUserCurrent.mobile = userEdit_Mobile.Text;
            objUserCurrent.email = userEdit_Email.Text;
            objUserCurrent.address = userEdit_Address.Text;

            if (userIdCurrent == null)
            {
                APIManager.UserCreate(objUserCurrent);
            }
            else
            {
                objUserCurrent.userId = this.userIdCurrent;
                objUserCurrent.active = userEdit_switActive.Checked;
                APIManager.UserUpdate(objUserCurrent);
            }

            //Task<StatusResponse> statusResponse= userId == string.Empty ? APIManager.UserCreate(objUser) : APIManager.UserUpdate(objUser);            

            OnBackPressed();
        }

        private void Delete()
        {
            APIManager.UserDelete(userIdCurrent);
            OnBackPressed();
        }

        private void ResetPassword()
        {
            StartActivity(new Intent(Application.Context, typeof(ResetPasswordActivity)));
        }

        private void ChagePassword()
        {
            StartActivity(new Intent(Application.Context, typeof(ResetPasswordActivity)));
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

            userIdCurrent = Intent.GetStringExtra("userId");

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

            //Reset password
            userEdit_linearResetPassword = FindViewById<LinearLayout>(Resource.Id.userEdit_linearResetPassword);
            userEdit_txtResetPassword = FindViewById<TextView>(Resource.Id.userEdit_txtResetPassword);
            userEdit_txtResetPassword.Click += UserEdit_txtResetPassword_Click;

            //Change password
            userEdit_linearChangePassword = FindViewById<LinearLayout>(Resource.Id.userEdit_linearChangePassword);
            userEdit_txtChangePassword = FindViewById<TextView>(Resource.Id.userEdit_txtChangePassword);
            userEdit_txtChangePassword.Click += UserEdit_txtChangePassword_Click; ;
        }

        private async void UserEdit_txtChangePassword_Click(object sender, EventArgs e)
        {
            //Get admin userId
            User objUserAdmin = AppInstance.user;
            string adminId = objUserAdmin.userId;

            SmartHome.Model.Message objMessage = await APIManager.ResetPasswordUser(adminId, objUserCurrent.userId, objUserCurrent.name);
            if (objMessage != null)
            {
                Toast.MakeText(this, string.Format("New password: {0}", objMessage.message), ToastLength.Long).Show();
            }
        }

        /// <summary>
        /// chỉ có admin reset pass của user thuộc role user, admin k tự reset pass của mình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UserEdit_txtResetPassword_Click(object sender, EventArgs e)
        {
            //Get admin userId
            User objUserAdmin = AppInstance.user;
            string adminId = objUserAdmin.userId;

            SmartHome.Model.Message objMessage = await APIManager.ResetPasswordUser(adminId, objUserCurrent.userId, objUserCurrent.name);
            if (objMessage != null)
            {
                Toast.MakeText(this, string.Format("New password: {0}", objMessage.message), ToastLength.Long).Show();
            }
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
                case Resource.Id.userEdit_mnuResetPw:
                    ResetPassword();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }



        #endregion
    }

}