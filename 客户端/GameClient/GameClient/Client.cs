using System;
using System.Collections.Generic;
using System.IO;
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
        private static StreamWriter mySW;
        private static UdpClient SendUdpClient, RecvUdpClient;
        private static Thread sendThread, recvThread;
        private static Semaphore mySendSem;
        private static Byte[] SendBytes, RecvBytes;
        private static string Ipaddr;
        private static Player[] myPlayers;
        public string IPAddr {
            get { return Ipaddr; }
            set { Ipaddr = value; }
        }
        private static int SendPort = 6667;
        private static int RecvPort = 6666;
        public static void Begin()
        {
            mySendSem = new Semaphore(0, 1);
            myPlayers = new Player[10];
            Ipaddr = "10.211.55.30";
            SendUdpClient = new UdpClient(SendPort);
            RecvUdpClient = new UdpClient(RecvPort);
            RecvBytes = new byte[320];
            mySendSem.Release();
            sendThread = new Thread(new ThreadStart(SendFunc));
            recvThread = new Thread(new ThreadStart(RecvFunc));
            sendThread.Start();
            recvThread.Start();
        }
        private static void SendFunc()
        {
            while (true)
            {
                mySendSem.WaitOne();
                try
                {
                    GetSendInfor(); 
                    SendUdpClient.Connect(Ipaddr, SendPort);
                    int t = SendUdpClient.Send(SendBytes, SendBytes.Length);
                    MessageBox.Show("已发送"+t.ToString()+"个字节!");
                }
                catch (Exception e)
                {
                    DateTime curTime = new DateTime();
                    mySW.WriteLine(curTime.ToString() + "Send函数失败，退出线程！" + "错误原因：" + e.ToString());
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
                    //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(Ipaddr), RecvPort);
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    RecvBytes = Client.RecvUdpClient.Receive(ref RemoteIpEndPoint);
                    MessageBox.Show(RecvBytes.ToString());
                    GetRecvInfor();
                }
                catch (Exception e)
                {
                    DateTime curTime = new DateTime();
                    mySW.WriteLine(curTime.ToString() + "Recv函数失败，退出线程！" + "错误原因：" + e.ToString());
                    break;
                }
            }
        }
        private static void GetRecvInfor()
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
        private static void GetSendInfor()
        {
            SendBytes = new Byte[320];
            for (int i = 0; i < SendBytes.Length; i++)
                SendBytes[i] = 1;
        }
        public Client()
        {
            mySW = new StreamWriter("GameClient.log");
        }
        ~Client()
        {
            SendUdpClient.Close();
            RecvUdpClient.Close();
            sendThread.Join();
            recvThread.Join();
            mySW.Close();
        }
    }
}
