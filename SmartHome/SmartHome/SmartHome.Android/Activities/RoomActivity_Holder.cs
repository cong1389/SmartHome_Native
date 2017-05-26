
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using System.Threading.Tasks;
using SmartHome.Util;
using SmartHome.Service.Response;
using SmartHome.Service;
using SmartHome.Model;
using System.Collections.Generic;
using SmartHome.Droid.Common;
using System;
using Android.Content;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "Smart Home - Room")]
    public class RoomActivity_Holder : AppCompatActivity
    {
        #region Parameter

        string houseId = string.Empty;

        List<Room> lstRoom = null;

        // RecyclerView instance that displays the photo album:
        RecyclerView room_recyclerView;

        // Layout manager that lays out each card in the RecyclerView:
        RecyclerView.LayoutManager layoutManager;

        RoomHolderAdapter room_Adapter;

        SwipeRefreshLayout room_swipe;

        #endregion

        #region Common

        private async Task GetData()
        {
            room_swipe.Refreshing = true;

            try
            {
                await room_Adapter.GetData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting acquaintances: {ex.Message}");

                //set alert for executing the task
                var alert = new Android.App.AlertDialog.Builder(this);

                alert.SetTitle("Error getting acquaintances");

                alert.SetMessage("Ensure you have a network connection, and that a valid backend service URL is present in the app settings.");

                alert.SetNegativeButton("OK", (senderAlert, args) =>
                {
                    // an empty delegate body, because we just want to close the dialog and not take any other action
                });

                //run the alert in UI thread to display in the screen
                RunOnUiThread(() =>
                {
                    alert.Show();
                });
            }
            finally
            {
                room_swipe.Refreshing = false;
            }
        }

        #endregion

        #region Event

        protected override async void OnResume()
        {
            base.OnResume();
            
            houseId = Intent.GetStringExtra("houseId");

            await GetData();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RoomLayout_Holer);

            //Set toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            // ensure that the system bar color gets drawn
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            //TextView room_txtUserName = FindViewById<TextView>(Resource.Id.room_txtUserName);
            //room_txtUserName.Text = AppInstance.user.username.ToUpper();

            room_swipe = (SwipeRefreshLayout)FindViewById(Resource.Id.room_swipe);

            room_swipe.Refresh += async (sender, e) =>
            {
                await GetData();
            };

            room_swipe.Post(() => room_swipe.Refreshing = true);

            room_recyclerView = FindViewById<RecyclerView>(Resource.Id.room_recyclerView);

            layoutManager = new LinearLayoutManager(this);

            // Plug the layout manager into the RecyclerView:
            room_recyclerView.SetLayoutManager(layoutManager);

            //............................................................
            // Adapter Setup:

            // Create an adapter for the RecyclerView, and pass it the
            // data set (the photo album) to manage:
            room_Adapter = new RoomHolderAdapter(lstRoom, this, houseId);

            // Register the item click handler (below) with the adapter:
            //  room_Adapter.ItemClick += OnItemClick;

            // Plug the adapter into the RecyclerView:
            room_recyclerView.SetAdapter(room_Adapter);

        }

        void OnItemClick(object sender, int position)
        {
            RoomAdapter dapter = (RoomAdapter)sender;
            //string userId = dapter != null ? dapter.lstRoom[position].userId : string.Empty;

            //var userEditActivity = new Intent(this, typeof(UserEditActivity));
            //userEditActivity.PutExtra("userId", userId);
            //StartActivity(userEditActivity);

            //// Display a toast that briefly shows the enumeration of the selected photo:
            //int photoNum = position + 1;
            //Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.RoomBar, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.RoomBar_mnuSave).SetVisible(false);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.RoomBar_mnuCreateRoom:
                    //var roomCreateActivity = new Intent(this, typeof(RoomCreateActivity));
                    //roomCreateActivity.PutExtra("houseId", houseId);
                    StartActivity(GetCreateActivity());
                    break;

                case Resource.Id.RoomBar_mnuCreateDevice:
                    var deviceCreateActivity = new Intent(this, typeof(DeviceCreateActivity));
                    deviceCreateActivity.PutExtra("houseId", houseId);
                    StartActivity(deviceCreateActivity);
                    break;
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        Intent GetCreateActivity()
        {
            var createActivity=new Intent(this, typeof(RoomCreateActivity));
            createActivity.PutExtra("houseId", houseId);

            return createActivity;
        }


        #endregion

    }

    internal class RoomHolderAdapter : RecyclerView.Adapter
    {
        string houseId = string.Empty;

        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;
        
        public List<Room> lstRoom;

        Activity activity;

        // Load the adapter with the data set (photo album) at construction time:        
        public RoomHolderAdapter(List<Room> lstRoom, Activity activity, string houseId)
        {
            this.lstRoom = lstRoom;
            this.activity = activity;
            this.houseId = houseId;
        }

        public async Task GetData()
        {
            //Lấy obj house đã lưu trước đó
            House objHouse_Result = AppInstance.houseData;

            //Nếu house trước đó == NULL, thì gọi lại API GetHouse
            if (objHouse_Result == null)
            {
                List<House> lstHouse = await APIManager.GetHouseByHouseId(houseId);
                objHouse_Result = lstHouse[0];
                lstRoom = objHouse_Result.rooms;
            }
            else
            {
                lstRoom = objHouse_Result.rooms;
            }

            NotifyDataSetChanged();        
        }

        // Create a new photo CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.RoomLayoutItem_Holder, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            RoomViewHolder vh = new RoomViewHolder(itemView, OnClick);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override async void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RoomViewHolder vh = holder as RoomViewHolder;

            vh.roomLayoutItem_txtRoomName.Text = lstRoom[position].name;

            //Bind list view device
            Room objRoom = await APIManager.GetDeviceByRoomId(houseId, lstRoom[position].roomId);
            if (objRoom != null)
            {
                List<Devices> lstDevice = objRoom.devices;
                vh.roomLayoutItem_lstDevice.Adapter = new DeviceListItemAdapter(activity, lstDevice, houseId, lstRoom[position].roomId);

                vh.roomLayoutItem_txtDeviceCount.Text = string.Format("Device: {0}", lstDevice.Count);
            }
        }

        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return lstRoom == null ? -1 : lstRoom.Count; }
        }

        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

    //----------------------------------------------------------------------
    // VIEW HOLDER

    // Implement the ViewHolder pattern: each ViewHolder holds references
    // to the UI components (ImageView and TextView) within the CardView 
    // that is displayed in a row of the RecyclerView:
    internal class RoomViewHolder : RecyclerView.ViewHolder
    {
        public TextView roomLayoutItem_txtRoomName { get; private set; }
        public TextView roomLayoutItem_txtDeviceCount { get; private set; }
        public ListView roomLayoutItem_lstDevice { get; private set; }

        // Get references to the views defined in the CardView layout.
        public RoomViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:           
            roomLayoutItem_txtRoomName = itemView.FindViewById<TextView>(Resource.Id.roomLayoutItem_txtRoomName);
            roomLayoutItem_txtDeviceCount = itemView.FindViewById<TextView>(Resource.Id.roomLayoutItem_txtDeviceCount);
            roomLayoutItem_lstDevice = itemView.FindViewById<ListView>(Resource.Id.roomLayoutItem_lstDevice);

            // Detect user clicks on the item view and report which item
            // was clicked (by position) to the listener:
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }
}