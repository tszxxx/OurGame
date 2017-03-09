using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Game
    {
        /// <summary>
        /// 总的游戏服务器
        /// </summary>
        private int [][] myMap;
        private Server myServer;
        public void Init()
        {
            myServer = new Server();
            myServer.Begin();
        }
    }
}
