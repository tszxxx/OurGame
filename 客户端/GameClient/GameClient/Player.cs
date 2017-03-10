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
            get { return Y; }
            set { Y = value; }
        }
        public String PlayerToString(){
            String mystring;
            mystring = hp.ToString() + " ";
            mystring += mp.ToString() + " ";
            mystring += pd.ToString() + " ";
            mystring += md.ToString() + " ";
            mystring += pr.ToString() + " ";
            mystring += mr.ToString() + " ";
            mystring += x.ToString() + " ";
            mystring += y.ToString() + " ";
            return mystring;
        }
        public void StringToPlayer(String mystring)
        {
            int[] index = new int[10];
            index[0] = mystring.IndexOf(' ', 0);
            for (int i = 1; i < 10; i++)
                index[i] = mystring.IndexOf(' ', index[i - 1] + 1);
            hp = int.Parse(mystring.Substring(0, index[0] - 1));
            mp = int.Parse(mystring.Substring(index[0] + 1, index[1] - index[0]));
            pd = int.Parse(mystring.Substring(index[1] + 1, index[2] - index[1]));
            md = int.Parse(mystring.Substring(index[2] + 1, index[3] - index[2]));
            pr = int.Parse(mystring.Substring(index[3] + 1, index[4] - index[3]));
            mr = int.Parse(mystring.Substring(index[4] + 1, index[5] - index[4]));
            x = int.Parse(mystring.Substring(index[5] + 1, index[6] - index[5]));
            y = int.Parse(mystring.Substring(index[6] + 1, index[7] - index[6]));
        }
    }
}
