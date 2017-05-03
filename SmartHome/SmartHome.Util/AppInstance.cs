using SmartHome.Model;

namespace SmartHome.Util
{
    public static class AppInstance
    {
        #region Parameter

        //Get width screen
        //  public static double wScreen = App.Current.MainPage.Width;

        public static User user { get; set; }
        public static House house { get; set; }

        #endregion

        #region Lưu data để sử dụng page khác

        //public static HouseResponseCollection Data_HouseResponseCollection { get; set; }
        //public static StatusResponse Data_DeviceStatusResponse { get; set; }

        #endregion

        #region Địa chỉ API
        
        public static string api = "http://magnetapptest.herokuapp.com";

        public static string api_LoginByUser = string.Format("{0}/{1}", api, "api/user/login");

        public static string api_HouseAll = string.Format("{0}/{1}", api, "api/house/getAll");
        public static string api_HouseByUser = string.Format("{0}/{1}", api, "api/house/getHouseByUserId");
        public static string api_HouseGet = string.Format("{0}/{1}", api, "api/house/get");

        public static string api_RoomByRoomId = string.Format("{0}/{1}", api, "api/room/get");

        public static string api_DeviceActive = string.Format("{0}/{1}", api, "api/device/active");
        public static string api_DeviceDeActive = string.Format("{0}/{1}", api, "api/device/deactive");

        public static string api_DeviceTestOn = string.Format("{0}/{1}", api, "api/Manager/testOn");
        public static string api_DeviceTestOff = string.Format("{0}/{1}", api, "api/Manager/testOff");

        public static string api_hello = string.Format("{0}/{1}", api, "api/hello");

        #endregion
    }
}
