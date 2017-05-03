
using Android.App;
using Android.OS;
using Android.Support.V7;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Widget;
using Android.Views;

namespace SmartHome.Droid
{
    //[Activity (Label = "Smart Home", MainLauncher = true, Icon = "@drawable/icon")]
    [Activity]
    public class MainActivity : AppCompatActivity
    {
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{          
            //base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            //SetContentView (Resource.Layout.Login);

            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
           // SetSupportActionBar(toolbar);

            //SupportActionBar.Title = "Smart Home";

            //var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar_bottom);

            //toolbarBottom.Title = "Menu";
            //toolbarBottom.InflateMenu(Resource.Menu.photo_edit);

            //toolbarBottom.MenuItemClick += (sender, e) =>
            //{
            //    Toast.MakeText(this, "Bottom toolbar pressed: " + e.Item.TitleFormatted, ToastLength.Short).Show();
            //};


            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button> (Resource.Id.myButton);

            //button.Click += delegate {
            //	button.Text = string.Format ("{0} clicks!", count++);
            //};
        }

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.home, menu);
        //    return base.OnCreateOptionsMenu(menu);
        //}

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    Toast.MakeText(this, "Top Action pressed:" + item.TitleFormatted, ToastLength.Short).Show();
        //    return base.OnOptionsItemSelected(item);
        //}
    }
}


