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
    class DeviceAdapter : BaseAdapter<Devices>
    {
        Activity currentContext;
        List<Devices> lstDevices;
        string houseId = string.Empty;
        string roomId = string.Empty;

        public DeviceAdapter(Activity currentContext, List<Devices> lstDevice, string houseId, string roomId)
        {
            this.currentContext = currentContext;
            this.lstDevices = lstDevice;
            this.houseId = houseId;
            this.roomId = roomId;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = lstDevices[position];

            if (convertView == null)
                convertView = currentContext.LayoutInflater.Inflate(Resource.Layout.DeviceGridViewItem, null);

            convertView.FindViewById<TextView>(Resource.Id.txtName).Text = item.name;
            //else
            //{
            //    convertView.FindViewById<TextView>(Resource.Id.txtName).Text = item.name;
            //    //convertView.FindViewById<TextView>(Resource.Id.txtHouseId).Text = item.houseId;
            //    //convertView.FindViewById<ImageView>(Resource.Id.img).SetImageResource(Resource.Drawable.monkey);

            //    //Lưu value sử dụng khi change value Switch
            //    //Dictionary<object, string> dic = new Dictionary<object, string>();
            //    //dic.Add("houseId", houseId);
            //    //dic.Add("roomId", roomId);
            //    //dic.Add("deviceId", item.deviceId);


            //    Xamarin.Forms.ResourceDictionary dic = new Xamarin.Forms.ResourceDictionary();
            Switch switch1 = convertView.FindViewById<Switch>(Resource.Id.switch1);
            switch1.Checked = bool.Parse(item.status);
            switch1.Tag = item.deviceId;
            switch1.CheckedChange += switcher_Toggled;
            //}

            return convertView;
        }

        private void switcher_Toggled(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Switch switch1 = (Switch)sender;
            string deviceId = (string)switch1.Tag;

            Switche(houseId, roomId, deviceId, e.IsChecked);
        }

        private async Task Switche(string houseId, string roomId, string deviceId, bool status)
        {
            await APIManager.TestOn(houseId, roomId, deviceId, status);
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstDevices == null ? -1 : lstDevices.Count;
            }
        }

        public override Devices this[int position]
        {
            get
            {
                return lstDevices == null ? null : lstDevices[position];
            }
        }
    }
}