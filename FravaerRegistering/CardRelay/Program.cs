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

            IPAddress ip = IPAddress.Any;
            IPEndPoint remote = new IPEndPoint(ip, ipind);

            Console.WriteLine("Server Blocked");
            while (true)
            {
                try
                {
                    byte[] reciveData = Reciver.Receive(ref remote);
                    string DecodedData = Encoding.ASCII.GetString(reciveData);
                    Console.WriteLine(DecodedData);
                    


                    UdpClient Sender = new UdpClient(remote.Address.ToString(), ipout);
                    byte[] send = Encoding.ASCII.GetBytes("CARD/" + DecodedData.Split('/')[2] + "/COMFIRMED");
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
