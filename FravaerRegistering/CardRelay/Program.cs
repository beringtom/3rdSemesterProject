using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CardRelay
{
    class Program
    {
        static void Main(string[] args)
        {

            int ipind = 8547;
            int ipout = 9876;

            UdpClient Reciver = new UdpClient(ipind);

            IPAddress endip = IPAddress.Any;
            IPEndPoint remote = new IPEndPoint(endip, ipind);

            Console.WriteLine("Waiting for Numbers");
            while (true)
            {
                try
                {
                    byte[] reciveData = Reciver.Receive(ref remote);
                    string DecodedData = Encoding.ASCII.GetString(reciveData);
                    Console.WriteLine(DecodedData);

                    string[] SplitDecodedData = DecodedData.Split('/');
                    string Url = "http://restfravaerservice.azurewebsites.net/service1.svc/Sensor/";

                    

                    SensorData sdata = new SensorData(SplitDecodedData[1], SplitDecodedData[2], SplitDecodedData[3]);
                    string response = AddSensordata(sdata ,Url).Result;



                    UdpClient Sender = new UdpClient(remote.Address.ToString(), ipout);
                    byte[] send = Encoding.ASCII.GetBytes("CARD/" + response + "/" + SplitDecodedData[3]);
                    Sender.Send(send, send.Length);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            
        }




        private static async Task<string> AddSensordata(object s, string uri)
        {
            using (HttpClient client = new HttpClient())
            {

                var jsonString = JsonConvert.SerializeObject(s);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                string returnString = JsonConvert.DeserializeObject<string>(str);
                return returnString;
            }
        }
    }
}
