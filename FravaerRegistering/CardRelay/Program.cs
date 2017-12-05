using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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


                    string SensorUrl = "";


                    UdpClient Sender = new UdpClient(remote.Address.ToString(), ipout);
                    byte[] send = Encoding.ASCII.GetBytes("CARD/" + "CHECKIN/" + DecodedData.Split('/')[2]);
                    Sender.Send(send, send.Length);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
