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
using SmartHome.Service.Response;
using SmartHome.Model;

namespace SmartHome.Droid.Common
{
    class HouseAdapter : BaseAdapter<House>
    {
        Activity currentContext;
        List<House> lstHouse;        

        public HouseAdapter(Activity currentContext, List<House> lstHouse)
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
                convertView = currentContext.LayoutInflater.Inflate(Resource.Layout.HouseGridViewItem, null);
            //else
            //{
                convertView.FindViewById<TextView>(Resource.Id.txtName).Text = item.name;
                convertView.FindViewById<TextView>(Resource.Id.txtHouseId).Text = item.houseId;
                //convertView.FindViewById<ImageView>(Resource.Id.img).SetImageResource(Resource.Drawable.monkey);
            //}

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