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

        public static async Task<StatusResponse> UserAddDeleteRole(string userId, string roleId,bool isAdd)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                HttpContent str = new StringContent(string.Format("userId={0}&roleId={1}", userId,roleId), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = isAdd == true ? await client.PutAsync(AppInstance.api_UserAddRole, str) : await client.PutAsync(AppInstance.api_UserRemoveRole, str);
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
        
        public static async Task<StatusResponse> IsUserSetActive(string userId, bool statusCurrent)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                HttpContent str = new StringContent(string.Format("userId={0}", userId), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = statusCurrent == true ? await client.PutAsync(AppInstance.api_UserActive, str) : await client.PutAsync(AppInstance.api_UserDeActive, str);
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

        public static LoginResponse loginResponse { get; set; }

        public static async Task<User> GetUserByUserId(string userId)
        {
            User objUser = new User();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(string.Format("{0}/{1}", AppInstance.api_UserGetByUserId, userId));
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    objUser = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<User>(result) : null;
                }
            }
            catch (Exception ex)
            {
            }

            return objUser;
        }

        public static async Task<List<User>> GetUserAll()
        {
            List<User> lstUser = new List<User>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(AppInstance.api_UserAll);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    lstUser = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<User>>(result) : null;
                }
            }
            catch (Exception ex)
            {
            }

            return lstUser;
        }

        public static async Task<LoginResponse> GetUserByUsrAndPw(string usr, string pw)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("x-app-id", "test");

                var str = new StringContent(string.Format("username={0}&password={1}&appTokenId={2}", usr, pw,"test"), Encoding.UTF8, "application/x-www-form-urlencoded");
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

        public static async Task<StatusResponse> UserCreate(User objUser)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var str = new StringContent(string.Format("name={0}&deviceId={1}&tenantId={2}&username={3}&password={4}&mobile={5}&email={6}&address={7}"
                    , objUser.name, objUser.deviceId, objUser.tenantId, objUser.username, objUser.password, objUser.mobile, objUser.email, objUser.address), Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.PostAsync(new Uri(AppInstance.api_UserCreate), str);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return statusResponse;
        }

        public static async Task<StatusResponse> UserUpdate(User objUser)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var str = new StringContent(string.Format("userId={0}&name={1}&mobile={2}&email={3}&address={4}&active={5}"
                    , objUser.userId, objUser.name, objUser.mobile, objUser.email, objUser.address, objUser.active), Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.PutAsync(new Uri(AppInstance.api_UserUpdate), str);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return statusResponse;
        }

        public static async Task<StatusResponse> UserDelete(string userId)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.DeleteAsync(string.Format("{0}/{1}", AppInstance.api_UserDelete, userId));
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                }
            }
            catch (Exception ex)
            {
            }

            return statusResponse;
        }

        public static async Task<StatusResponse> GetUserAddHouse(string houseId, string userId, bool statusCurrent)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!statusCurrent)
                {
                    HttpContent str = new StringContent(string.Format("houseId={0}&userId={1}", houseId, userId), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = await client.PutAsync(AppInstance.api_UserRemoveHouse, str);
                    var statusCode = response.StatusCode; //get status return from api 
                    if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                    }
                }
                else
                {
                    HttpContent str = new StringContent(string.Format("houseId={0}&userId={1}", houseId, userId), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = await client.PutAsync(AppInstance.api_UserAddHouse, str);
                    var statusCode = response.StatusCode; //get status return from api 
                    if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                    }
                }                
            }
            catch (Exception ex)
            {
            }

            return statusResponse;
        }

        public static async Task<Message> ResetPasswordUser(string adminId, string userId, string username)
        {
            SmartHome.Model.Message objMessage = new SmartHome.Model.Message();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent str = new StringContent(string.Format("adminId={0}&userId={1}&username={2}", adminId, userId, username), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.PutAsync(AppInstance.api_UserResetPassword, str);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    objMessage = JsonConvert.DeserializeObject<Message>(result);
                }
            }
            catch (Exception ex)
            {
            }

            return objMessage;
        }

        public static async Task<Message> ChangePasswordUser(string username, string oldPassword, string newPassword,string userId)
        {
            SmartHome.Model.Message objMessage = new SmartHome.Model.Message();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent str = new StringContent(string.Format("username={0}&oldPassword={1}&newPassword={2}&userId={3}", username, oldPassword, newPassword, userId), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.PutAsync(AppInstance.api_UserChangePassword, str);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    objMessage = JsonConvert.DeserializeObject<Message>(result);
                }
            }
            catch (Exception ex)
            {
            }

            return objMessage;
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
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

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

        public static async Task<List<House>> GetHouseByHouseId(string houseId)
        {
            List<House> lstHouse = new List<House>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(string.Format("{0}/{1}", AppInstance.api_HouseGet, houseId));
                string result = response.Content.ReadAsStringAsync().Result;

                lstHouse = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<House>>(result) : null;
                AppInstance.houseData = lstHouse[0];
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return lstHouse;

            //try
            //{
            //    var client = new HttpClient();
            //    client.BaseAddress = new Uri(AppInstance.api);
            //    client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

            //    var response = await client.GetAsync(string.Format("{0}/{1}", AppInstance.api_HouseGet, houseId));
            //    string result = response.Content.ReadAsStringAsync().Result;

            //    HouseResponseCollection houseResponse = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<HouseResponseCollection>(result) : null;
            //    AppInstance.houseData = houseResponseCollection.data[0];
            //    //AppInstance.houseData = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<House>(result) : null;
            //}
            //catch (Exception ex)
            //{
            //    string msg = ex.Message;
            //}

            //return AppInstance.houseData;
        }

        public static async Task<List<House>> GetHouseAll()
        {
            List<House> lstHouse = new List<House>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(AppInstance.api_HouseGetAll);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    //string messge = await response.RequestMessage.Content.ReadAsStringAsync(); //get message return from api
                    //checke message here before deserialize object
                    var result = response.Content.ReadAsStringAsync().Result;
                    lstHouse = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<House>>(result) : null;
                }
            }
            catch (Exception ex)
            {
            }

            return lstHouse;
        }

        #endregion

        #region Room

        public static async Task<Room> GetRoomByRoomId(string roomId)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

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

        public static async Task<StatusResponse> RoomUpdate(string houseId,string roomId,string roomName)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var str = new StringContent(string.Format("houseId={0}&roomId={1}&name={2}", houseId, roomId, roomName), Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PutAsync(new Uri(AppInstance.api_RoomUpdate), str);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return statusResponse;
        }

        public static async Task<StatusResponse> RoomCreate(string houseId, string roomName)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var str = new StringContent(string.Format("houseId={0}&name={1}", houseId, roomName), Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(new Uri(AppInstance.api_RoomCreate), str);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return statusResponse;
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
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

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
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

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

        public static async Task<StatusResponse> IsDeviceSetPaired(string houseId, string roomId, string deviceId, bool statusCurrent)
        {
            Status status = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                HttpContent str = new StringContent(string.Format("houseId={0}&roomId={1}&deviceId={2}", houseId, roomId, deviceId), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = statusCurrent == true ? await client.PutAsync(AppInstance.api_DevicePaired, str) : await client.PutAsync(AppInstance.api_DeviceUnsetPaired, str);
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
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

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

        #region Company

        /// <summary>
        /// Product hind
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static async Task<List<Product>> GetCompanyFilter(string companyId)
        {
            List<Product> lstProduct = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(string.Format("{0}?companyId={1}", AppInstance.api_GetCompanyFilter, companyId));
                string result = response.Content.ReadAsStringAsync().Result;

                lstProduct = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<Product>>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return lstProduct;
        }

        public static async Task<List<Company>> GetCompanyAll()
        {
            List<Company> lstCompany = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(AppInstance.api_GetCompany);
                string result = response.Content.ReadAsStringAsync().Result;

                lstCompany = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<Company>>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return lstCompany;
        }

        #endregion        

        #region ProductType

        public static async Task<List<ProductType>> GetProductTypeAll()
        {
            List<ProductType> lstProductType = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(AppInstance.api_GetProductType);
                string result = response.Content.ReadAsStringAsync().Result;

                lstProductType = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<ProductType>>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return lstProductType;
        }

        public static async Task<List<ProductType>> GetProductTypeByProducTypeId(string productTypeId)
        {
            List<ProductType> lstProductType = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(string.Format("{0}/{1}", AppInstance.api_GetProductTypeByProducTypeId, productTypeId));                
                string result = response.Content.ReadAsStringAsync().Result;

                lstProductType = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<ProductType>>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return lstProductType;
        }


        #endregion

        #region Product
        public static async Task<List<Product>> GetProducByCompanyId(string companyId)
        {
            List<Product> lstProduct = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var response = await client.GetAsync(string.Format("{0}?companyId={1}", AppInstance.api_GetProductByCompanyId, companyId));
                string result = response.Content.ReadAsStringAsync().Result;

                lstProduct = !string.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<List<Product>>(result) : null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return lstProduct;
        }

        #endregion

        #region Manager

        public static async Task<Room> SetUpDeviceBehavior(string userId, string houseId, string roomId, string companyId, string productId,string deviceName)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppInstance.api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-access-token", AppInstance.user.accessToken);

                var str = new StringContent(string.Format("userId={0}&houseId={1}&roomId={2}&companyId={3}&productId={4}&name={5}"
                    , userId, houseId, roomId, companyId, productId, deviceName), Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(new Uri(AppInstance.api_SetUpDeviceBehavior), str);
                var statusCode = response.StatusCode; //get status return from api 
                if (statusCode == HttpStatusCode.OK && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    statusResponse = JsonConvert.DeserializeObject<StatusResponse>(result);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return room;
        }

        #endregion

    }
}
