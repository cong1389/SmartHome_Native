using SmartHome.Model;

namespace SmartHome.Util
{
    public static class AppInstance
    {
        #region Parameter

        //Get width screen
        //  public static double wScreen = App.Current.MainPage.Width;

        public static User user { get; set; }
        public static House houseData { get; set; }
        public static Room roomData { get; set; }

        #endregion

        #region Lưu data để sử dụng page khác

        //public static HouseResponseCollection Data_HouseResponseCollection { get; set; }
        //public static StatusResponse Data_DeviceStatusResponse { get; set; }

        #endregion

        #region Địa chỉ API

        public static string api = "http://magnetapptest.herokuapp.com";

        public static string api_UserAll = string.Format("{0}/{1}", api, "api/user/get");
        public static string api_LoginByUser = string.Format("{0}/{1}", api, "api/user/login");
        public static string api_UserActive = string.Format("{0}/{1}", api, "api/user/active");
        public static string api_UserDeActive = string.Format("{0}/{1}", api, "api/user/deactive");
        public static string api_UserGetByUserId = string.Format("{0}/{1}", api, "api/user/get");
        public static string api_UserCreate = string.Format("{0}/{1}", api, "api/user/create");
        public static string api_UserUpdate = string.Format("{0}/{1}", api, "api/user/update");
        public static string api_UserDelete = string.Format("{0}/{1}", api, "api/user/delete");
        public static string api_UserAddHouse = string.Format("{0}/{1}", api, "api/user/addHouse");
        public static string api_UserRemoveHouse = string.Format("{0}/{1}", api, "api/user/removehouse");

        public static string api_HouseGetAll = string.Format("{0}/{1}", api, "api/house/get");
        public static string api_HouseByUser = string.Format("{0}/{1}", api, "api/house/getHouseByUserId");
        public static string api_HouseGet = string.Format("{0}/{1}", api, "api/house/get");

        public static string api_UserResetPassword = string.Format("{0}/{1}", api, "api/user/resetPassword");
        public static string api_UserChangePassword = string.Format("{0}/{1}", api, "api/user/changePassword");

        //User role
        public static string api_UserAddRole = string.Format("{0}/{1}", api, "api/user/addAdminRole");
        public static string api_UserRemoveRole = string.Format("{0}/{1}", api, "api/user/removeAdminRole");

        public static string api_RoomByRoomId = string.Format("{0}/{1}", api, "api/room/get");

        public static string api_DeviceActive = string.Format("{0}/{1}", api, "api/device/active");
        public static string api_DeviceDeActive = string.Format("{0}/{1}", api, "api/device/deactive");

        public static string api_DeviceTestOn = string.Format("{0}/{1}", api, "api/Manager/testOn");
        public static string api_DeviceTestOff = string.Format("{0}/{1}", api, "api/Manager/testOff");

        public static string api_hello = string.Format("{0}/{1}", api, "api/hello");

        #endregion
    }
}
