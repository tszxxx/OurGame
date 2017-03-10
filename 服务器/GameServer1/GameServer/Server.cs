using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace GameServer
{
    public class Server
    {
        private static Manager myManager;
        private static int sendPort = 8888;
        private static int receivePort = 6667;
        /* Add by BBge - Bgein*/
        private static bool isConnected = false;
        private static bool isReceived = false;
        private UdpClient udpcSend;
        private UdpClient udpcReceive;
        /* Add by BBge - End*/
        public Server(){
            myManager = new Manager();
            
        }
        private static void Send(Object my)
        {
            while (true)
            {
                UdpClient myUdpClient = my as UdpClient;
                if (myUdpClient == null)
                    myUdpClient = new UdpClient(sendPort);
                try
                {
                    myUdpClient.Connect("127.0.0.1", sendPort);
                    // String sendString = myManager.MYPlayers[1].PlayerToString();
                    Byte[] sendBytes = Encoding.Default.GetBytes("i'm from server");
                    myUdpClient.Send(sendBytes, sendBytes.Length);
                    //MessageBox.Show("发送成功");
                    /* Add by BBge - Bgein*/
                    isConnected = true;
                    /* Add by BBge - End*/

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    /* Add by BBge - Bgein*/
                    isConnected = false;
                    /* Add by BBge - End*/
                }
            }
        }
        public static void Receive(object my)
        {
            UdpClient myUdpClient = my as UdpClient;
            if (myUdpClient == null)
                myUdpClient = new UdpClient(6667);
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                // Blocks until a message returns on this socket from a remote host.
                //MessageBox.Show("0");
                Byte[] receiveBytes = myUdpClient.Receive(ref RemoteIpEndPoint);
                //MessageBox.Show("1");
                string returnData = Encoding.Unicode.GetString(receiveBytes);
                /* Add by BBge - Bgein*/
                isReceived = true;
                MessageBox.Show("服务器接收到了客户端的消息");
                
                /* Add by BBge - End*/

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                /* Add by BBge - Bgein*/
                isReceived = false;
                /* Add by BBge - End*/
            }
        }
        public void Begin()
        {
            IPEndPoint allIpep = new IPEndPoint(
                    IPAddress.Parse("127.0.0.1"), receivePort); // 本机IP和监听端口号
            udpcReceive = new UdpClient(allIpep);
            udpcSend = new UdpClient(sendPort);
            
            Thread mainReceiveServer = new Thread(new ParameterizedThreadStart(Receive));
            Thread mainSendServer = new Thread(new ParameterizedThreadStart(Send));
            
            mainReceiveServer.IsBackground = true;
            mainSendServer.IsBackground = true;
            mainReceiveServer.Start(udpcReceive);
            mainSendServer.Start(udpcSend);
        }
        public string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            //txtLocalIP.Text = AddressIP;
            Console.WriteLine(AddressIP);
            return AddressIP;
        }
        public void detect()
        {
            //检测是否有玩家和小兵出现
        }
    }
}
