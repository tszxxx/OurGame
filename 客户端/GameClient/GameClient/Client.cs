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
        private static UdpClient SendUdpClient, RecvUdpClient;
        private static Thread sendThread, recvThread;
        private static Semaphore mySendSem;
        private static Byte[] SendBytes, RecvBytes;
        private static string Ipaddr;
        private static Player[] myPlayers;
        private static Byte CurPlayer;
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
            CurPlayer = 0;
            Ipaddr = "10.211.55.30";
            SendUdpClient = new UdpClient(SendPort);
            RecvUdpClient = new UdpClient(RecvPort);
            SendBytes = new byte[320];
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
                    SendPlayers();
                    SendUdpClient.Connect(Ipaddr, SendPort);
                    int t = SendUdpClient.Send(SendBytes, SendBytes.Length);
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
                    //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(Ipaddr), RecvPort);
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    RecvBytes = Client.RecvUdpClient.Receive(ref RemoteIpEndPoint);
                    MessageBox.Show(RecvBytes.ToString());
                    RecvPlayers();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
        }
        private static void RecvPlayers()
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
        private static void SendPlayers()
        {
            for (int i = 0; i < 10; i++)
            {
                SendBytes[0 + 32 * i] = (Byte)(myPlayers[i].HP >> 24);
                SendBytes[1 + 32 * i] = (Byte)(myPlayers[i].HP >> 16);
                SendBytes[2 + 32 * i] = (Byte)(myPlayers[i].HP >> 8);
                SendBytes[3 + 32 * i] = (Byte)myPlayers[i].HP;
                SendBytes[4 + 32 * i] = (Byte)(myPlayers[i].MP >> 24);
                SendBytes[5 + 32 * i] = (Byte)(myPlayers[i].MP >> 16);
                SendBytes[6 + 32 * i] = (Byte)(myPlayers[i].MP >> 8);
                SendBytes[7 + 32 * i] = (Byte)myPlayers[i].MP;
                SendBytes[8 + 32 * i] = (Byte)(myPlayers[i].PD >> 24);
                SendBytes[9 + 32 * i] = (Byte)(myPlayers[i].PD >> 16);
                SendBytes[10 + 32 * i] = (Byte)(myPlayers[i].PD >> 8);
                SendBytes[11 + 32 * i] = (Byte)myPlayers[i].PD;
                SendBytes[12 + 32 * i] = (Byte)(myPlayers[i].MD >> 24);
                SendBytes[13 + 32 * i] = (Byte)(myPlayers[i].MD >> 16);
                SendBytes[14 + 32 * i] = (Byte)(myPlayers[i].MD >> 8);
                SendBytes[15 + 32 * i] = (Byte)myPlayers[i].MD;
                SendBytes[16 + 32 * i] = (Byte)(myPlayers[i].PR >> 24);
                SendBytes[17 + 32 * i] = (Byte)(myPlayers[i].PR >> 16);
                SendBytes[18 + 32 * i] = (Byte)(myPlayers[i].PR >> 8);
                SendBytes[19 + 32 * i] = (Byte)myPlayers[i].PR;
                SendBytes[20 + 32 * i] = (Byte)(myPlayers[i].MR >> 24);
                SendBytes[21 + 32 * i] = (Byte)(myPlayers[i].MR >> 16);
                SendBytes[22 + 32 * i] = (Byte)(myPlayers[i].MR >> 8);
                SendBytes[23 + 32 * i] = (Byte)myPlayers[i].MR;
                SendBytes[24 + 32 * i] = (Byte)(myPlayers[i].X >> 24);
                SendBytes[25 + 32 * i] = (Byte)(myPlayers[i].X >> 16);
                SendBytes[26 + 32 * i] = (Byte)(myPlayers[i].X >> 8);
                SendBytes[27 + 32 * i] = (Byte)myPlayers[i].X;
                SendBytes[28 + 32 * i] = (Byte)(myPlayers[i].Y >> 24);
                SendBytes[29 + 32 * i] = (Byte)(myPlayers[i].Y >> 16);
                SendBytes[30 + 32 * i] = (Byte)(myPlayers[i].Y >> 8);
                SendBytes[31 + 32 * i] = (Byte)myPlayers[i].Y;
            }
        }
        private static void UseMagic(int MagicNum)
        {
            switch (MagicNum)
            {
                case 0: myPlayers[CurPlayer].HP = 0; myPlayers[CurPlayer + 1].HP = 200; mySendSem.Release(); break;
                case 1: myPlayers[CurPlayer].MP = 0; myPlayers[CurPlayer + 1].HP = 200; mySendSem.Release(); break;
                case 2: myPlayers[CurPlayer].PD = 0; myPlayers[CurPlayer + 1].HP = 200; mySendSem.Release(); break;
                case 3: myPlayers[CurPlayer].MD = 0; myPlayers[CurPlayer + 1].HP = 200; mySendSem.Release(); break;
                case 4: myPlayers[CurPlayer].PR = 0; myPlayers[CurPlayer + 1].HP = 200; mySendSem.Release(); break; 
                default:break;
            }
        }
        public Client()
        {
        }
        ~Client()
        {
            SendUdpClient.Close();
            RecvUdpClient.Close();
            sendThread.Join();
            recvThread.Join();
        }
    }
}
