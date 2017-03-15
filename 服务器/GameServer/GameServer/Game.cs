using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Game
    {
        private Server myServer;
        public string Init()
        {
            Server myServer = new Server();
            myServer.Begin();
            return myServer.GetAddressIP();
        }

    }
}
