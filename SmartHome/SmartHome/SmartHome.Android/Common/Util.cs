using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using SmartHome.Service;

namespace SmartHome.Droid.Common
{
    public static class Util
    {
        static bool isCheckController = false;

        public async static Task<bool> IsCheckController()
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