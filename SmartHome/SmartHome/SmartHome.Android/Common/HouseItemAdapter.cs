﻿using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using SmartHome.Model;
using SmartHome.Service;
using System.Threading.Tasks;

namespace SmartHome.Droid.Common
{
    class HouseItemAdapter : BaseAdapter<House>
    {
        Activity currentContext;
        List<House> lstHouse,lstHouseOld;
        string userId = string.Empty;

        public HouseItemAdapter(Activity currentContext, List<House> lstHouse,string userId, List<House> lstHouseOld)
        {
            this.currentContext = currentContext;
            this.lstHouse = lstHouse;
            this.userId = userId;
            this.lstHouseOld = lstHouseOld;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = lstHouse[position];

            if (convertView == null)
                convertView = currentContext.LayoutInflater.Inflate(Resource.Layout.UserEdit_HouseItem, null);

            convertView.FindViewById<TextView>(Resource.Id.userEdit_HouseItem_txtHouse).Text = item.name;

            Switch switch1 = convertView.FindViewById<Switch>(Resource.Id.userEdit_HouseItem_switch);
            //set status house
            if (lstHouseOld != null)
            {
                for (int i = 0; i < lstHouseOld.Count; i++)
                {
                    switch1.Checked = lstHouseOld[i].houseId == item.houseId ? true : false;
                }
            }
            switch1.Tag = item.houseId;
            switch1.CheckedChange += switcher_Toggled;

           

            return convertView;
        }

        private async void switcher_Toggled(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Switch switch1 = (Switch)sender;
            string houseId = (string)switch1.Tag;

            await APIManager.GetUserAddHouse(houseId, userId, e.IsChecked);
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstHouse == null ? -1 : lstHouse.Count;
            }
        }

        public override House this[int position]
        {
            get
            {
                return lstHouse == null ? null : lstHouse[position];
            }
        }
    }
}