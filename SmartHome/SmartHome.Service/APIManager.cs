using Newtonsoft.Json;
using SmartHome.Service.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SmartHome.Model;
using SmartHome.Util;
using System.Text;
using System.Net.Http.Headers;

namespace SmartHome.Service
{
    public class APIManager
    {
        #region Parameter

        public static HouseResponseCollection houseResponseCollection { get; set; }
        //public static House house { get; set; }

        public static Room room { get; set; }

        #endregion

        #region User

        public static LoginResponse loginResponse { get; set; }

        public static async Task<LoginResponse> GetUserByUsrAndPw(string usr, string pw)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-app-token", "test");

                var str = new StringContent(string.Format("username={0}&password={1}", usr, pw), Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.PostAsync(new Uri(AppInstance.api_LoginByUser), str);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    string messge = await response.RequestMessage.Content.ReadAsStringAsync(); //get message return from api
                                                                                               //checke message here before deserialize object
                    var result = response.Content.ReadAsStringAsync().Result;

                    loginResponse = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<LoginResponse>(result) : null;
                    if (loginResponse != null)
                    {
                        AppInstance.user = loginResponse;
                        //AppInstance.appToken = loginResponse.acccessToken;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return loginResponse;
            //return AppInstance.Data_HouseResponseCollection;
        }

        #endregion

        #region House        


        public static async Task<HouseResponseCollection> GetHouseByUser(string userId)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                var response = await client.GetAsync(string.Format("{0}/{1}", AppInstance.api_HouseByUser, userId));
                string result = response.Content.ReadAsStringAsync().Result;

                houseResponseCollection = JsonConvert.DeserializeObject<HouseResponseCollection>(result);
            }
            catch (Exception ex)
            {
                //houseResponseCollection.Error = ex.Message;
            }

            return houseResponseCollection;
            //return AppInstance.Data_HouseResponseCollection;
        }

        public static async Task<List<House>> GetHouseByHouseId_Test(string houseId)
        {
            List<House> lstHouse = new List<House>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                var response = await client.GetAsync(string.Format("{0}/{1}", AppInstance.api_HouseGet, houseId));
                string result = response.Content.ReadAsStringAsync().Result;

                lstHouse = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<House>>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return lstHouse;
        }

        public static async Task<House> GetHouseByHouseId(string houseId)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.acccessToken);

                var response = await client.GetAsync(string.Format("{0}/{1}", AppInstance.api_HouseGet, houseId));
                string result = response.Content.ReadAsStringAsync().Result;

                AppInstance.houseData = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<House>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return AppInstance.houseData;
        }

        public static async Task<HouseResponseCollection> GetHouseAll()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                var response = await client.GetAsync(AppInstance.api_HouseAll);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    //string messge = await response.RequestMessage.Content.ReadAsStringAsync(); //get message return from api
                    //checke message here before deserialize object
                    var result = response.Content.ReadAsStringAsync().Result;

                    houseResponseCollection = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<HouseResponseCollection>(result) : null;
                }
            }
            catch (Exception ex)
            {
            }

            return houseResponseCollection;
        }

        #endregion

        #region Room

        public static async Task<Room> GetRoomByRoomId(string roomId)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.acccessToken);

                var response = await client.GetAsync(string.Format("{0}/{1}", AppInstance.api_RoomByRoomId, roomId));
                string result = response.Content.ReadAsStringAsync().Result;

                AppInstance.roomData = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<Room>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return AppInstance.roomData;
        }

        #endregion

        #region Device

        public static StatusResponse statusResponse { get; set; }

        public static async Task<Room> GetDeviceByRoomId(string houseId, string roomId)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.acccessToken);

                var response = await client.GetAsync(string.Format("{0}/{1}/{2}", AppInstance.api_RoomByRoomId, houseId, roomId));
                string result = response.Content.ReadAsStringAsync().Result;

                room = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<Room>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return room;
        }

        /// <summary>
        /// Set Active device
        /// </summary>
        /// <param name="houseId">House Id</param>
        /// <param name="roomId">Room Id</param>
        /// <param name="deviceId">Device Id</param>
        /// <returns></returns>
        public static async Task<StatusResponse> IsDeviceSetStatus(string houseId, string roomId, string deviceId, bool statusCurrent)
        {
            Status status = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.acccessToken);

                HttpContent str = new StringContent(string.Format("houseId={0}&roomId={1}&deviceId={2}", houseId, roomId, deviceId), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = statusCurrent == true ? await client.PutAsync(AppInstance.api_DeviceActive, str) : await client.PutAsync(AppInstance.api_DeviceDeActive, str);
                var statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                }
            }
            catch (Exception ex)
            {
                //houseResponseCollection.Error = ex.Message;
            }

            //return houseResponseCollection;
            return statusResponse;
        }

        public static async Task<StatusResponse> TestOn(string houseId, string roomId, string deviceId, bool statusCurrent)
        {
            Status status = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.acccessToken);

                HttpContent str = new StringContent("deviceBehaviorId=\"a6eceaf0-18f8-11e7-97f1-1789c5b3403e\"", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                //var response = await client.PostAsync(AppInstance.api_DeviceTestOn, null);

                var response = statusCurrent == true ? await client.PostAsync(AppInstance.api_DeviceTestOn, null) : await client.PutAsync(AppInstance.api_DeviceTestOff, null);
                var statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                }
            }
            catch (Exception ex)
            {
                //houseResponseCollection.Error = ex.Message;
            }

            //return houseResponseCollection;
            return statusResponse;
        }

        #endregion

        #region Hello

        public static async Task<bool> GetHello(string ip)
        {
            bool resultResponse = false;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                //client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.acccessToken);

                var response = await client.GetAsync(AppInstance.api_hello);
                var statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    resultResponse = true;
                    //var result = response.Content.ReadAsStringAsync().Result;
                    //statusResponse = JsonConvert.DeserializeObject<Hello>(result);
                    //AppInstance.house = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<Hello>(result) : null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return resultResponse;
        }

        #endregion

    }
}
