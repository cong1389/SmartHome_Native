
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
using System;
using Android.Content;
using Android.Views;
using Java.Util;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "Smart Home - Create Device", ParentActivity = typeof(RoomActivity))]
    public class DeviceCreateActivity : AppCompatActivity
    {
        #region Parameter

        string userId = string.Empty, houseId = string.Empty, roomId = string.Empty, companyId = string.Empty, productId = string.Empty;

        Spinner deviceCreate_spinCompany = null;
        List<Company> lstCompany = null;
        List<Room> lstRoom = null;
        List<ProductType> lstProductType = null;

        EditText deviceCreate_txtProductName;

        #endregion

        #region Common

        #endregion

        #region Event

        protected override async void OnResume()
        {
            base.OnResume();

            userId = AppInstance.user.userId;

            houseId = Intent.GetStringExtra("houseId");

            deviceCreate_txtProductName = FindViewById<EditText>(Resource.Id.deviceCreate_txtProductName);

            //Company spinter
            deviceCreate_spinCompany = FindViewById<Spinner>(Resource.Id.deviceCreate_spinCompany);
            lstCompany = await APIManager.GetCompanyAll();
            var companyAdapter = new CompanyAdapter(this, lstCompany);
            deviceCreate_spinCompany.Adapter = companyAdapter;
            deviceCreate_spinCompany.ItemSelected += DeviceCreate_spinCompany_ItemSelected;

            //ProductType spinter
            Spinner deviceCreate_spinProductType = FindViewById<Spinner>(Resource.Id.deviceCreate_spinProductType);
            lstProductType = await APIManager.GetProductTypeAll();
            var producTypeAdap = new ProductTypeAdapter(this, lstProductType);
            deviceCreate_spinProductType.Adapter = producTypeAdap;
            deviceCreate_spinProductType.ItemSelected += DeviceCreate_spinProductType_ItemSelectedAsync;

            //room spinter
            Spinner deviceCreate_spinRoom = FindViewById<Spinner>(Resource.Id.deviceCreate_spinRoom);
            List<House> lstHouse = await APIManager.GetHouseByHouseId(houseId);
            if (lstHouse !=null)
            {
                lstRoom= lstHouse[0].rooms;
                var roomAdap = new RoomSpinAdapter(this, lstRoom);
                deviceCreate_spinRoom.Adapter = roomAdap;
                deviceCreate_spinRoom.ItemSelected += DeviceCreate_spinRoom_ItemSelected;
            }
        }

        private void DeviceCreate_spinRoom_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spin = (Spinner)sender;
            roomId = this.lstRoom[e.Position].roomId;
        }

        private async void DeviceCreate_spinCompany_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spin = (Spinner)sender;
            companyId = this.lstCompany[e.Position].companyId;

            Spinner deviceCreate_spinProductHind = FindViewById<Spinner>(Resource.Id.deviceCreate_spinProductHind);
            List<ProductType> lstProductType = await APIManager.GetCompanyFilter(companyId);
            var producTypeAdap = new ProductTypeAdapter(this, lstProductType);
            deviceCreate_spinProductHind.Adapter = producTypeAdap;
        }

        private async void DeviceCreate_spinProductType_ItemSelectedAsync(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spin = (Spinner)sender;
            productId = this.lstProductType[e.Position].productTypeId;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DeviceCreateLayout);

            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            Title = "CREATE DEVICE";
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.RoomBar, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.RoomBar_mnuCreateRoom).SetVisible(false);
                menu.FindItem(Resource.Id.RoomBar_mnuCreateDevice).SetVisible(false);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.RoomBar_mnuSave:
                    DeviceCreate();
                    break;

            }

            return base.OnOptionsItemSelected(item);
        }

        private async void DeviceCreate()
        {
            var alert = new Android.App.AlertDialog.Builder(this);

            if (string.IsNullOrWhiteSpace(deviceCreate_txtProductName.Text) || string.IsNullOrWhiteSpace(deviceCreate_txtProductName.Text))
            {
                alert.SetTitle("Invalid product name!");

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
            string productName = deviceCreate_txtProductName.Text;

            await APIManager.SetUpDeviceBehavior(userId, houseId, roomId, companyId,productId, productName);

            //OnBackPressed();
        }


        #endregion

    }
}