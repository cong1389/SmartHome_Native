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
    class ProductAdapter : BaseAdapter<Product>
    {
        Activity currentContext;
        List<Product> lstProduct;
        string houseId = string.Empty;
        string roomId = string.Empty;

        public ProductAdapter(Activity currentContext, List<Product> lstProduct)
        {
            this.currentContext = currentContext;
            this.lstProduct = lstProduct;
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
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = lstProduct[position].name;
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstProduct == null ? -1 : lstProduct.Count;
            }
        }

        public override Product this[int position]
        {
            get
            {
                return lstProduct == null ? null : lstProduct[position];
            }
        }
    }
}