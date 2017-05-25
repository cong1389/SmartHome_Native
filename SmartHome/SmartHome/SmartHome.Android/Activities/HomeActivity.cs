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
using Android.Content;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using SmartHome.Droid.Fragments;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "Smart Home DashBoard")]
    public class HomeActivity : AppCompatActivity
    {
        #region Parameter

        UserResponseCollection houseResponseCollection;

        GridView grdHouse;
        Toolbar toolbar_bottom;

        #endregion

        #region Common


        #endregion

        #region Event

        DrawerLayout drawerLayout;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.Main);
                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

                // Init toolbar
                var toolbar = FindViewById<Toolbar>(Resource.Id.app_bar);
                SetSupportActionBar(toolbar);
                SupportActionBar.SetTitle(Resource.String.app_name);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetDisplayShowHomeEnabled(true);

                // Toolbar bottom
                toolbar_bottom = FindViewById<Toolbar>(Resource.Id.toolbar_bottom);
                toolbar_bottom.Title = "Menu";
                toolbar_bottom.InflateMenu(Resource.Menu.photo_edit);

                //headerdrawerlayout.
                // Attach item selected handler to navigation view
                var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                var nav_usr = navigationView.FindViewById<TextView>(Resource.Id.nav_usr);
                navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

                // Create ActionBarDrawerToggle button and add it to the toolbar
                var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
                drawerLayout.SetDrawerListener(drawerToggle);
                drawerToggle.SyncState();

                if (savedInstanceState == null)
                {
                    ListItemClicked(1);
                    navigationView.SetCheckedItem(Resource.Id.nav_home);
                }
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }
        }

        protected override void OnResume()
        {
            SupportActionBar.SetTitle(Resource.String.app_name);
            base.OnResume();
        }

        private void ListItemClicked(int position)
        {
            Fragment fragment = null;
            switch (position)
            {
                case 1:
                    fragment = new HouseFragment();
                    toolbar_bottom.Visibility = ViewStates.Visible;
                    break;
                //case 1:
                //    fragment = new RoomFragment();                   
                //    break;
                case 2:
                    fragment = new DeviceFragment();
                    break;              
            }

            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Replace(Resource.Id.HomeFrameLayout, fragment);
            ft.Commit();
        }

        //define action for navigation menu selection
        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {                
                case (Resource.Id.nav_home):
                    ListItemClicked(1);
                    break;                
                case (Resource.Id.nav_userlist):
                    StartActivity(new Intent(Application.Context, typeof(UserListActivity_ViewHolder)));
                    break;
                case (Resource.Id.nav_device):
                    StartActivity(new Intent(Application.Context, typeof(RoomActivity)));
                    break;
                case (Resource.Id.nav_logout):
                    StartActivity(new Intent(Application.Context, typeof(LoginActivity)));
                    break;
            }
            // Close drawer
            drawerLayout.CloseDrawers();
        }

        //add custom icon to tolbar
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.action_refresh).SetVisible(true);
                menu.FindItem(Resource.Id.action_attach).SetVisible(false);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        //define action for tolbar icon press
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //this.Activity.Finish();
                    return true;
                case Resource.Id.action_attach:
                    //FnAttachImage();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        //to avoid direct app exit on backpreesed and to show fragment from stack
        public override void OnBackPressed()
        {
            if (FragmentManager.BackStackEntryCount != 0)
            {
                FragmentManager.PopBackStack();// fragmentManager.popBackStack();
            }
            else
            {
                base.OnBackPressed();
            }
        }

        #endregion

    }
}