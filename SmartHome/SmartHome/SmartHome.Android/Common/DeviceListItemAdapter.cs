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
    class DeviceListItemAdapter : BaseAdapter<Devices>
    {
        Activity currentContext;

        List<Devices> lstDevices;
        string houseId = string.Empty;
        string roomId = string.Empty;

        public DeviceListItemAdapter(Activity currentContext, List<Devices> lstDevice, string houseId, string roomId)
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
                convertView = currentContext.LayoutInflater.Inflate(Resource.Layout.DeviceListItem, null);

            TextView txtName = convertView.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = item.name;
            txtName.Tag = item.deviceId;
            txtName.Click += TxtName_Click;
            
            return convertView;
        }

        private void TxtName_Click(object sender, System.EventArgs e)
        {
            TextView txtName = (TextView)sender;

            var deviceActivity = new Intent(currentContext, typeof(DeviceActivity));
            deviceActivity.PutExtra("deviceId", txtName.Tag.ToString());
            deviceActivity.PutExtra("houseId", houseId);
            deviceActivity.PutExtra("roomId", roomId);
            deviceActivity.PutExtra("roomName",txtName.Text);
            currentContext.StartActivity(deviceActivity);

            //Toast.MakeText(currentContext, "adfas", ToastLength.Long).Show();
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