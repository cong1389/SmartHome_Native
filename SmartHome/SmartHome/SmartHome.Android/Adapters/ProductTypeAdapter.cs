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

namespace SmartHome.Droid.Common
{
    class ProductTypeAdapter : BaseAdapter<ProductType>
    {
        Activity currentContext;
        List<ProductType> lstProductType;
        string houseId = string.Empty;
        string roomId = string.Empty;

        public ProductTypeAdapter(Activity currentContext, List<ProductType> lstProductType)
        {
            this.currentContext = currentContext;
            this.lstProductType = lstProductType;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null) // otherwise create a new one
            {
                view = currentContext.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = lstProductType[position].name;
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstProductType == null ? -1 : lstProductType.Count;
            }
        }

        public override ProductType this[int position]
        {
            get
            {
                return lstProductType == null ? null : lstProductType[position];
            }
        }
    }
}