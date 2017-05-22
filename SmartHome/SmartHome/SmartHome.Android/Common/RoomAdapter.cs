using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using SmartHome.Model;
using System.Threading.Tasks;
using SmartHome.Service;
using System.Linq;
using SmartHome.Droid.Fragments;

namespace SmartHome.Droid.Common
{
    class RoomAdapter : BaseAdapter<Room>
    {
        Activity currentContext;

        List<Room> lstRoom;
        List<Devices> lstDevices;

        string houseId;

        public RoomAdapter(Activity currentContext, List<Room> lstRoom, string houseId)
        {
            this.currentContext = currentContext;
            this.lstRoom = lstRoom;
            this.houseId = houseId;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        private async Task GetDeviceDataAsync(string roomId, View convertView)
        {
            Room objRoom = await APIManager.GetDeviceByRoomId(houseId, roomId);
            if (objRoom != null)
            {
                lstDevices = objRoom.devices;
                ListView listViewDevice = convertView.FindViewById<ListView>(Resource.Id.listViewDevice);
                listViewDevice.Adapter = new DeviceListItemAdapter(currentContext, lstDevices, houseId, roomId);

            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = lstRoom[position];

            if (convertView == null)
                convertView = currentContext.LayoutInflater.Inflate(Resource.Layout.RoomGridViewItem, null);

            TextView txtName = convertView.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = item.name;
            txtName.Tag = item.roomId;
            txtName.Click += TxtName_Click;

            TextView RoomGridItem_txtDeviceCount = convertView.FindViewById<TextView>(Resource.Id.RoomGridItem_txtDeviceCount);
            GetDeviceDataAsync(houseId, item.roomId, RoomGridItem_txtDeviceCount);
            
            RoomGridItem_txtDeviceCount.Text = string.Format("Device: {0}", lstRoom.Count.ToString());

            GetDeviceDataAsync(item.roomId, convertView);

            return convertView;
        }

        private async Task GetDeviceDataAsync(string houseId, string roomId, TextView RoomGridItem_txtDeviceCount)
        {
            Room objRoom = await APIManager.GetDeviceByRoomId(houseId, roomId);
            if (objRoom != null)
            {
                List<Devices> lstDevice = objRoom.devices;
                RoomGridItem_txtDeviceCount.Text = string.Format("Device: {0}", lstDevice.Count());
            }
        }

        private void TxtName_Click(object sender, System.EventArgs e)
        {
            TextView txtName = (TextView)sender;
            string roomId = txtName.Tag.ToString();

            FragmentTransaction fragmentTransaction = currentContext.FragmentManager.BeginTransaction();

            Fragment fragmentPrev = currentContext.FragmentManager.FindFragmentByTag("dialog");
            if (fragmentPrev != null)
                fragmentTransaction.Remove(fragmentPrev);

            fragmentTransaction.AddToBackStack(null);
            //create and show the dialog
            RoomEditFragment dialogFragment =new RoomEditFragment(houseId, roomId);
            dialogFragment.Show(fragmentTransaction, "dialog");
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