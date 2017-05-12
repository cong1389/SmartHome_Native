using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using SmartHome.Model;
using System.Threading.Tasks;
using SmartHome.Service;

namespace SmartHome.Droid.Common
{
    class UserAdapter : BaseAdapter<User>
    {
        Activity currentContext;
        List<User> lstUser;

        public UserAdapter(Activity currentContext, List<User> lstUser)
        {
            this.currentContext = currentContext;
            this.lstUser = lstUser;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = lstUser[position];

            if (convertView == null)
                convertView = currentContext.LayoutInflater.Inflate(Resource.Layout.UserListItem, null);
            //else
            //{
            convertView.FindViewById<TextView>(Resource.Id.userList_txtName).Text = item.name;
            //convertView.FindViewById<TextView>(Resource.Id.txtHouseId).Text = item.houseId;
            //convertView.FindViewById<ImageView>(Resource.Id.img).SetImageResource(Resource.Drawable.monkey);
            //}

            //Switch switch1 = convertView.FindViewById<Switch>(Resource.Id.switch1);
            //switch1.Checked = item.active;
            //switch1.Tag = item.userId;
            //switch1.CheckedChange += switcher_Toggled;

            return convertView;
        }

        //private void switcher_Toggled(object sender, CompoundButton.CheckedChangeEventArgs e)
        //{
        //    Switch switch1 = (Switch)sender;
        //    string id = (string)switch1.Tag;

        //    Switche(id, e.IsChecked);
        //}

        //private async Task Switche(string userId,  bool status)
        //{
        //    await APIManager.IsUserSetActive(userId,  status);
        //}

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstUser == null ? -1 : lstUser.Count;
            }
        }

        public override User this[int position]
        {
            get
            {
                return lstUser == null ? null : lstUser[position];
            }
        }
    }
}