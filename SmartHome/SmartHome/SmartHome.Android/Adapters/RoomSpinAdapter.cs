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
    class RoomSpinAdapter : BaseAdapter<Room>
    {
        Activity currentContext;
        List<Room> lstRoom;
        string houseId = string.Empty;
        string roomId = string.Empty;

        public RoomSpinAdapter(Activity currentContext, List<Room> lstRoom)
        {
            this.currentContext = currentContext;
            this.lstRoom = lstRoom;
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
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = lstRoom[position].name;
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstRoom == null ? -1 : lstRoom.Count;
            }
        }

        public override Room this[int position]
        {
            get
            {
                return lstRoom == null ? null : lstRoom[position];
            }
        }
    }
}