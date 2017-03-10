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
        private static Thread sendThread, recvThread;
        private static Semaphore mySem = new Semaphore(0, 1);
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
        private static void SendFunc()
        {
            while (true)
            {
                mySem.WaitOne();
                try
                {
                    Byte[] SendBytes;
                    SendBytes = new Byte[100];
                    for (int i = 0; i < SendBytes.Length; i++)
                        SendBytes[i] = (byte) '1';
                    UdpClient.Connect(Ipaddr, port);
                    int t = UdpClient.Send(SendBytes, SendBytes.Length);
                    MessageBox.Show("已发送"+t.ToString()+"个字节!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
        }
        private static void RecvFunc()
        {
            while (true)
            {
                try
                {
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(Ipaddr), 11000);
                    Byte[] receiveBytes = Client.UdpClient.Receive(ref RemoteIpEndPoint);
                    MessageBox.Show(receiveBytes.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
        }
        public static void Begin()
        {
            Ipaddr = "10.211.55.30";
            UdpClient = new UdpClient(port);
            mySem.Release();
            sendThread = new Thread(new ThreadStart(SendFunc));
            recvThread = new Thread(new ThreadStart(RecvFunc));
            sendThread.Start();
            recvThread.Start();
        }
    }
}
