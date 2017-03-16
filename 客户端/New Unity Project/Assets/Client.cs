using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
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
            try
            {
                if (mySW != null)
                    mySW.Close();
                else mySW = new StreamWriter("GameClient.log");
                mySendSem = new Semaphore(0, 1);
                myPlayers = new Player[10];
                Ipaddr = "10.180.31.134";
                SendUdpClient = new UdpClient(SendPort);
                RecvUdpClient = new UdpClient(RecvPort);
                RecvBytes = new byte[320];
                sendThread = new Thread(new ThreadStart(SendFunc));
                recvThread = new Thread(new ThreadStart(RecvFunc));
                sendThread.Start();
                recvThread.Start();
            }
            catch (Exception e)
            {
                Debug.Log("Begin出错!" + e.ToString());
            }
        }
        private static void SendFunc()
        {
            while (true)
            {
                mySendSem.WaitOne();
                try
                {
                    SendUdpClient.Connect(Ipaddr, SendPort);
                    SendUdpClient.Send(SendBytes, SendBytes.Length);
                }
                catch (Exception e)
                {
                    DateTime curTime = new DateTime();
                    mySW.WriteLine(curTime.ToString() + "Send函数失败，退出线程！" + "错误原因：" + e.ToString());
                    mySW.Close();
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
                    GetRecvInfor();
                }
                catch (Exception e)
                {
                    DateTime curTime = new DateTime();
                    mySW.WriteLine(curTime.ToString() + "Recv函数失败，退出线程！" + "错误原因：" + e.ToString());
                    mySW.Close();
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
        private static void SetSendMeg(Byte[] mybytes)
        {
            SendBytes = (Byte[])mybytes.Clone();
        }
        public void UseMagicQ(int x, int y)
        {
            Byte[] mybytes = new byte[6];
            mybytes[0] = 0;
            mybytes[1] = (byte)'Q';
            mybytes[2] = (Byte)(x >> 8);
            mybytes[3] = (Byte)(x);
            mybytes[4] = (Byte)(y >> 8);
            mybytes[5] = (Byte)(y);
            SetSendMeg(mybytes);
        }
        public void UseMagicW(int x, int y)
        {
            Byte[] mybytes = new byte[6];
            mybytes[0] = 0;
            mybytes[1] = (byte)'W';
            mybytes[2] = (Byte)(x >> 8);
            mybytes[3] = (Byte)(x);
            mybytes[4] = (Byte)(y >> 8);
            mybytes[5] = (Byte)(y);
            SetSendMeg(mybytes);
        }
        public void UseMagicE(int x, int y)
        {
            Byte[] mybytes = new byte[6];
            mybytes[0] = 0;
            mybytes[1] = (byte)'E';
            mybytes[2] = (Byte)(x >> 8);
            mybytes[3] = (Byte)(x);
            mybytes[4] = (Byte)(y >> 8);
            mybytes[5] = (Byte)(y);
            SetSendMeg(mybytes);
        }
        public void UseMagicR(int x, int y)
        {
            Byte[] mybytes = new byte[6];
            mybytes[0] = 0;
            mybytes[1] = (byte)'R';
            mybytes[2] = (Byte)(x >> 8);
            mybytes[3] = (Byte)(x);
            mybytes[4] = (Byte)(y >> 8);
            mybytes[5] = (Byte)(y);
            SetSendMeg(mybytes);
        }
        public void PressMouse(int x, int y)
        {
            Byte[] mybytes = new byte[5];
            mybytes[0] = 1;
            mybytes[1] = (Byte)(x >> 8);
            mybytes[2] = (Byte)(x);
            mybytes[3] = (Byte)(y >> 8);
            mybytes[4] = (Byte)(y);
            SetSendMeg(mybytes);
            mySendSem.Release();
        }
        public void SetSendReady()
        {
            mySendSem.Release();
        }
        public Player getPlayer(int num)
        {
            return myPlayers[num];
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
            mySW.Close();
        }
    }
}
