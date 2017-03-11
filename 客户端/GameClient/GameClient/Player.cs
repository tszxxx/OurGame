using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class Player
    {
        /// <summary>
        /// 玩家信息
        /// </summary>
        private String ipAddress;
        public String IPAddress {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        private int hp;//人物生命值
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        private int mp;//人物法力值
        public int MP
        {
            get { return mp; }
            set { mp = value; }
        }
        private int pd;//人物物理攻击强度
        public int PD
        {
            get { return pd; }
            set { pd = value; }
        }
        private int md;//人物魔法攻击强度
        public int MD
        {
            get { return md; }
            set { md = value; }
        }
        private int pr;//人物物理防御强度
        public int PR
        {
            get { return pr; }
            set { pr = value; }
        }
        private int mr;//人物魔法防御强度
        public int MR
        {
            get { return mr; }
            set { mr = value; }
        }
        private int x, y;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
