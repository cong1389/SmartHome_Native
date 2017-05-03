using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using SmartHome.Model;

namespace SmartHome.Droid.Common
{
    class RoomAdapter : BaseAdapter<Room>
    {
        Activity currentContext;
        List<Room> lstRoom;

        public RoomAdapter(Activity currentContext, List<Room> lstHouse)
        {
            this.currentContext = currentContext;
            this.lstRoom = lstHouse;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = lstRoom[position];

            if (convertView == null)
                convertView = currentContext.LayoutInflater.Inflate(Resource.Layout.RoomGridViewItem, null);
            //else
            //{
            convertView.FindViewById<TextView>(Resource.Id.txtName).Text = item.name;
            //convertView.FindViewById<TextView>(Resource.Id.txtHouseId).Text = item.houseId;
            //convertView.FindViewById<ImageView>(Resource.Id.img).SetImageResource(Resource.Drawable.monkey);
            //}

            return convertView;
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