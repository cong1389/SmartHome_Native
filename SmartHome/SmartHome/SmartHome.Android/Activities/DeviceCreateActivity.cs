
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

        string houseId = string.Empty,companyId=string.Empty;

        Spinner deviceCreate_spinCompany = null;
        List<Company> lstCompany = null;
        #endregion

        #region Common

        #endregion

        #region Event

        protected override async void OnResume()
        {
            base.OnResume();

            houseId = Intent.GetStringExtra("houseId");

            //Company spinter
            deviceCreate_spinCompany = FindViewById<Spinner>(Resource.Id.deviceCreate_spinCompany);
            lstCompany = await APIManager.GetCompanyAll();
            var companyAdapter = new CompanyAdapter(this, lstCompany);
            deviceCreate_spinCompany.Adapter = companyAdapter;
            deviceCreate_spinCompany.ItemSelected += DeviceCreate_spinCompany_ItemSelected;


            //ProductType spinter
            Spinner deviceCreate_spinProductType = FindViewById<Spinner>(Resource.Id.deviceCreate_spinProductType);
            List<ProductType> lstProductType = await APIManager.GetProductTypeAll();
            var producTypeAdap = new ProductTypeAdapter(this, lstProductType);
            deviceCreate_spinProductType.Adapter = producTypeAdap;
            deviceCreate_spinProductType.ItemSelected += DeviceCreate_spinProductType_ItemSelectedAsync; ;



        }

        private async void DeviceCreate_spinCompany_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spin = (Spinner)sender;
            companyId=   this.lstCompany[e.Position].name;                

            Spinner deviceCreate_spinProductHind = FindViewById<Spinner>(Resource.Id.deviceCreate_spinProductHind);            
            List<ProductType> lstProductType = await APIManager.GetCompanyFilter(companyId);
            var producTypeAdap = new ProductTypeAdapter(this, lstProductType);
            deviceCreate_spinProductHind.Adapter = producTypeAdap;
        }

        private async void DeviceCreate_spinProductType_ItemSelectedAsync(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            
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
                    //  RoomCreate();
                    break;

            }

            return base.OnOptionsItemSelected(item);
        }




        #endregion

    }
}