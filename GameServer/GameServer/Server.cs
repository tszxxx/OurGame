using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace GameServer
{
    public class Server
    {
        private Manager myManager;
        public Server(){
            myManager = new Manager();
            
        }
        public void Send(UdpClient myUdpClient)
        {
            if (myUdpClient == null)
                myUdpClient = new UdpClient(11000);
            try
            {
                myUdpClient.Connect("10.214.10.10", 11000);
                String sendString = myManager.MYPlayers[1].PlayerToString();
                Byte[] sendBytes = Encoding.Default.GetBytes(sendString);
                myUdpClient.Send(sendBytes, sendBytes.Length);
            }
            catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
        public void Receive(UdpClient myUdpClient)
        {
            if(myUdpClient == null)
                myUdpClient = new UdpClient(11000);
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = myUdpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
