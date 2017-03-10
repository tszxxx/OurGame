using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Manager
    {
        private Player[] myPlayers;
        public Player[] MYPlayers{
            get { return myPlayers; }
            set { myPlayers = value; }
        }
        public Manager(){
            myPlayers = new Player[10];
        }
        void AddPlayer(String IPAddress, int PlayerCount)
        {
            Player[] newPlayer = new Player[myPlayers.Length];
            String toPlayer;
            myPlayers.CopyTo(newPlayer, 0);
            newPlayer[myPlayers.Length].IPAddress = IPAddress;
            switch (PlayerCount)
            {
                case 0:toPlayer = "100 200 200 200 200 200"; break;
                case 1:toPlayer = "100 200 200 200 200 200"; break;
                default: toPlayer = "100 200 200 200 200 200"; break;
            }
            newPlayer[myPlayers.Length].StringToPlayer(toPlayer);
            myPlayers = newPlayer;
        }
        void DelPlayer(int PlayerCount)
        {
            Player[] newPlayer = new Player[myPlayers.Length - 2];
            for (int i = 0, count = 0; i < myPlayers.Length; i++)
                if (i != PlayerCount)
                    newPlayer[count++] = myPlayers[i];
            myPlayers = newPlayer;
        }
        ~Manager()
        {
            
        }
    }
}
