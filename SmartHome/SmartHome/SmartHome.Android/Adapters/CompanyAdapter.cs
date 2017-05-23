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
    class CompanyAdapter : BaseAdapter<Company>
    {
        Activity currentContext;
        List<Company> lstCompany;
        string houseId = string.Empty;
        string roomId = string.Empty;

        public CompanyAdapter(Activity currentContext, List<Company> lstCompany)
        {
            this.currentContext = currentContext;
            this.lstCompany = lstCompany;
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
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = lstCompany[position].name;
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstCompany == null ? -1 : lstCompany.Count;
            }
        }

        public override Company this[int position]
        {
            get
            {
                return lstCompany == null ? null : lstCompany[position];
            }
        }
    }
}