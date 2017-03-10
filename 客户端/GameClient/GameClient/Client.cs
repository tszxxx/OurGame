using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GameClient
{
    class Client
    {
        private static UdpClient UdpClient;
        private Thread sendThread, recvThread;
        private static string Ipaddr;
        public string IPAddr {
            get { return Ipaddr; }
            set { Ipaddr = value; }
        }
        private static int port = 6666;
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        public Client()
        {
        }
        private static void SendFunc(){
            try
            {
                Byte[] SendBytes;
                SendBytes = new Byte[100];
                for (int i = 0; i < SendBytes.Length; i++)
                    SendBytes[i] = 1;
                UdpClient.Connect(Ipaddr, port);
                UdpClient.Send(SendBytes, SendBytes.Length).ToString());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void RecvFunc()
        {
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(Ipaddr), 11000);
                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = Client.UdpClient.Receive(ref RemoteIpEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void Begin()
        {
            Ipaddr = "10.211.55.30";
            UdpClient = new UdpClient(port);
            sendThread = new Thread(new ThreadStart(SendFunc));
            recvThread = new Thread(new ThreadStart(RecvFunc));
            sendThread.Start();
            recvThread.Start();
        }
    }
}
