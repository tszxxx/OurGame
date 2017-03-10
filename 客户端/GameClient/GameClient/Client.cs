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
        private static Semaphore mySendSem, myPlayerSem;
        private static string Ipaddr;
        private static Player[] myPlayers;
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
                mySendSem.WaitOne();
                try
                {
                    Byte[] SendBytes;
                    SendBytes = new Byte[240];
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
                    RecvPlayers(receiveBytes);
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
            mySendSem = new Semaphore(0, 1);
            myPlayerSem = new Semaphore(0, 1);
            myPlayers = new Player[10];
            Ipaddr = "10.211.55.30";
            UdpClient = new UdpClient(port);
            mySendSem.Release();
            sendThread = new Thread(new ThreadStart(SendFunc));
            recvThread = new Thread(new ThreadStart(RecvFunc));
            sendThread.Start();
            recvThread.Start();
        }
        private static void RecvPlayers(Byte[] RecvBytes)
        {
            for (int i = 0; i < 10; i++)
            {
                myPlayers[i].HP = (RecvBytes[0 + 32 * i] << 24) + (RecvBytes[1 + 32 * i] << 16) + (RecvBytes[2 + 32 * i] << 8) + RecvBytes[3 + 32 * i];
                myPlayers[i].MP = (RecvBytes[4 + 32 * i] << 24) + (RecvBytes[5 + 32 * i] << 16) + (RecvBytes[6 + 32 * i] << 8) + RecvBytes[7 + 32 * i];
                myPlayers[i].PD = (RecvBytes[8 + 32 * i] << 24) + (RecvBytes[9 + 32 * i] << 16) + (RecvBytes[10 + 32 * i] << 8) + RecvBytes[11 + 32 * i];
                myPlayers[i].MD = (RecvBytes[12 + 32 * i] << 24) + (RecvBytes[13 + 32 * i] << 16) + (RecvBytes[14 + 32 * i] << 8) + RecvBytes[15 + 32 * i];
                myPlayers[i].PR = (RecvBytes[16 + 32 * i] << 24) + (RecvBytes[17 + 32 * i] << 16) + (RecvBytes[18 + 32 * i] << 8) + RecvBytes[19 + 32 * i];
                myPlayers[i].MR = (RecvBytes[20 + 32 * i] << 24) + (RecvBytes[21 + 32 * i] << 16) + (RecvBytes[22 + 32 * i] << 8) + RecvBytes[23 + 32 * i];
                myPlayers[i].X = (RecvBytes[24 + 32 * i] << 24) + (RecvBytes[25 + 32 * i] << 16) + (RecvBytes[26 + 32 * i] << 8) + RecvBytes[27 + 32 * i];
                myPlayers[i].Y = (RecvBytes[28 + 32 * i] << 24) + (RecvBytes[29 + 32 * i] << 16) + (RecvBytes[30 + 32 * i] << 8) + RecvBytes[31 + 32 * i];
            }
        }
        private static Byte[] SendPlayers()
        {
            Byte[] SendBytes = new byte[320];
            for (int i = 0; i < 10; i++)
            {
            }
            return SendBytes;
        }
    }
}
