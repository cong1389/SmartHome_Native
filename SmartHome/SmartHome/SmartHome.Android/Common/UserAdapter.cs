using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using SmartHome.Model;
using System.Threading.Tasks;
using SmartHome.Service;
using Android.Content;
using SmartHome.Droid.Activities;

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

            TextView userList_txtName = convertView.FindViewById<TextView>(Resource.Id.userList_txtName);
            userList_txtName.Text = item.name;
            userList_txtName.Tag = item.userId;
            userList_txtName.Click += UserList_txtName_Click;
            //convertView.FindViewById<TextView>(Resource.Id.userList_txtName).Text = item.name;

            List<House> lstHouse = item.houses;
            if (lstHouse !=null && lstHouse.Count>0)
            {
                ListView UserListItem_House = convertView.FindViewById<ListView>(Resource.Id.UserListItem_House);
                UserListItem_House.Adapter = new UserEdit_HouseItemAdapter(currentContext, lstHouse);
            }

            return convertView;
        }

        private void UserList_txtName_Click(object sender, System.EventArgs e)
        {
            TextView userList_txtName = (TextView)sender;
            string userId = userList_txtName.Tag.ToString();

            var userEditActivity = new Intent(currentContext, typeof(UserEditActivity));
            userEditActivity.PutExtra("userId", userId);
            currentContext.StartActivity(userEditActivity);
        }

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