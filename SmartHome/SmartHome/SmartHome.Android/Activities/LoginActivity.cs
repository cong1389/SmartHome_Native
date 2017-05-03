using Android.App;
using Android.OS;
using Android.Widget;
using SmartHome.Service.Response;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using SmartHome.Util;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Android.Content;
using System.IO;
using SmartHome.Service;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using SmartHome.Model;

namespace SmartHome.Droid.Activities
{
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        #region Parameter

        static CountdownEvent countdown;
        static int upCount = 0;
        static object lockObj = new object();
        bool resolveNames = true, isCheckController = false;

        EditText txtUsr, txtPw;

        #endregion

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            //Get control
            txtUsr = FindViewById<EditText>(Resource.Id.txtUsr);
            txtPw = FindViewById<EditText>(Resource.Id.txtPw);

            //Set default
            txtUsr.Text = "magnetdev";
            txtPw.Text = "12345";
            string token = "test";

            //Search ip của conller
            //await IsCheckController();

            //Event login
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin_Click;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            await GetLoginByUsr(txtUsr.Text, txtPw.Text);
        }

        private async Task<LoginResponse> GetLoginByUsr(string usr, string pw)
        {
            LoginResponse loginObject = null;
            try
            {
                //Search ip của conller
               // await IsCheckController();

                loginObject = await APIManager.GetUserByUsrAndPw(usr, pw);
                if (loginObject != null)
                {
                    // start the HomeActivity
                    StartActivity(new Intent(Application.Context, typeof(HomeActivity)));
                    //MainPage = new MasterDetailPage
                    //{
                    //    Master = new MasterPage(),
                    //    Detail = new NavigationPage(new HousePage())
                    //};
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return loginObject;
        }

        private async Task<bool> IsCheckController()
        {
            //bool result = false;

            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
            for (int i = 1; i <= 253; i++)
            {
                IAsyncResult iaResult = client.BeginConnect("192.168.0." + i.ToString(), 3000, null, null);
                iaResult.AsyncWaitHandle.WaitOne(200, false);
                if (client.Connected)
                {
                    isCheckController = await APIManager.GetHello("192.168.0." + i);
                    if (isCheckController)
                    {
                        arr.Add("192.168.0." + i);
                    }
                    break;
                }
            }
            return isCheckController;
        }
    }
}