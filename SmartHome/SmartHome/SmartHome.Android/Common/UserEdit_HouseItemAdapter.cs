using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using SmartHome.Model;
using SmartHome.Util;
using SmartHome.Service;
using Android.Content.Res;
using Android.Graphics;
using System.Threading.Tasks;
using SmartHome.Droid.Activities;
using Android.Content;

namespace SmartHome.Droid.Common
{
    class UserEdit_HouseItemAdapter : BaseAdapter<House>
    {
        Activity currentContext;

        List<House> lstHouse;

        public UserEdit_HouseItemAdapter(Activity currentContext, List<House> lstHouse)
        {
            this.currentContext = currentContext;
            this.lstHouse = lstHouse;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = lstHouse[position];

            if (convertView == null)
                convertView = currentContext.LayoutInflater.Inflate(Resource.Layout.DeviceListItem, null);

            TextView txtName = convertView.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = item.name;
            
            return convertView;
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