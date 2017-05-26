using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using SmartHome.Service;
using SmartHome.Model;
using SmartHome.Droid.Common;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "UserListActivity_ViewHolder", ParentActivity = typeof(HomeActivity))]
    public class UserListActivity_ViewHolder : AppCompatActivity
    {
        List<User> lstUser = null;

        // RecyclerView instance that displays the photo album:
        RecyclerView recyclerView;

        // Layout manager that lays out each card in the RecyclerView:
        RecyclerView.LayoutManager layoutManager;

        // Adapter that accesses the data set (a photo album):
        UserListAdapter userListAdapter;

        SwipeRefreshLayout swipeRefreshLayout;

        protected override async void OnResume()
        {
            base.OnResume();

            await GetData();
        }

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserListLayout_Holder);

            //Set toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            Title = "User Management";

            // ensure that the system bar color gets drawn
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            swipeRefreshLayout = (SwipeRefreshLayout)FindViewById(Resource.Id.acquaintanceListSwipeRefreshContainer);

            swipeRefreshLayout.Refresh += async (sender, e) => {
                await GetData();
            };

            swipeRefreshLayout.Post(() => swipeRefreshLayout.Refreshing = true);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            layoutManager = new LinearLayoutManager(this);

            // Plug the layout manager into the RecyclerView:
            recyclerView.SetLayoutManager(layoutManager);

            //............................................................
            // Adapter Setup:

            // Create an adapter for the RecyclerView, and pass it the
            // data set (the photo album) to manage:
            userListAdapter = new UserListAdapter(lstUser, this);

            // Register the item click handler (below) with the adapter:
            userListAdapter.ItemClick += OnItemClick;

            // Plug the adapter into the RecyclerView:
            recyclerView.SetAdapter(userListAdapter);

            var btnAddUser = (FloatingActionButton)FindViewById(Resource.Id.btnAddUser);
            btnAddUser.Click += (sender, e) =>
            {
                StartActivity(new Intent(Application.Context, typeof(UserEditActivity)));
            };
        }
        void OnItemClick(object sender, int position)
        {
            UserListAdapter dapter = (UserListAdapter)sender;
            string userId = dapter != null ? dapter.lstUser[position].userId : string.Empty;

            var userEditActivity = new Intent(this, typeof(UserEditActivity));
            userEditActivity.PutExtra("userId", userId);
            StartActivity(userEditActivity);

            //// Display a toast that briefly shows the enumeration of the selected photo:
            //int photoNum = position + 1;
            //Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();
        }

        private async Task GetData()
        {
            swipeRefreshLayout.Refreshing = true;

            try
            {
                await userListAdapter.GetData();
            }
            finally
            {
                swipeRefreshLayout.Refreshing = false;                
            }          
        }
    }

    public class UserListAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        //// Underlying data set (a photo album):
        public List<User> lstUser;

        Activity activity;

        // Load the adapter with the data set (photo album) at construction time:        
        public UserListAdapter(List<User> lstUser, Activity activity)
        {
            this.lstUser = lstUser;
            this.activity = activity;
        }

        public async Task GetData()
        {
            lstUser = await APIManager.GetUserAll();

            NotifyDataSetChanged();
        }

        // Create a new photo CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.UserListLayoutItem_Holder, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder vh = holder as PhotoViewHolder;
            
            vh.txtUserName.Text = lstUser[position].name;

            //Bind list view house
            List<House> lstHouse = lstUser[position].houses;
            if (lstHouse != null && lstHouse.Count > 0)
            {
                string roleName = lstUser[position].roles[0].ToString();
                vh.UserListItem_House.Adapter = new User_HouseItemAdapter(activity, lstHouse, roleName);
            }
        }

        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return lstUser == null ? -1 : lstUser.Count; }
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
    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public TextView txtUserName { get; private set; }
        public ListView UserListItem_House { get; private set; }

        // Get references to the views defined in the CardView layout.
        public PhotoViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:           
            txtUserName = itemView.FindViewById<TextView>(Resource.Id.txtUserName);
            UserListItem_House = itemView.FindViewById<ListView>(Resource.Id.UserListItem_House);

            // Detect user clicks on the item view and report which item
            // was clicked (by position) to the listener:
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }
}