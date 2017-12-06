using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

                    

                    SensorData sdata = new SensorData(SplitDecodedData[1], SplitDecodedData[2].Split('.')[0], SplitDecodedData[3].Split('\n')[0]);

                    string response = AddSensorData(sdata, Url);
                    Console.WriteLine(response);



                    UdpClient Sender = new UdpClient(remote.Address.ToString(), ipout);
                    byte[] send = Encoding.ASCII.GetBytes("CARD/" + response.Split('"')[1] + "/" + SplitDecodedData[3]);
                    Sender.Send(send, send.Length);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public static string AddSensorData(SensorData s, string url)
        {
            using (HttpClient client  = new HttpClient())
            {
                var jString = JsonConvert.SerializeObject(s);
                Console.WriteLine(jString);
                var stringContent = new StringContent(jString, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, stringContent);
                return response.Result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
