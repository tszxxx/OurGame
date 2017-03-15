using System;

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
        private int x, y, size;
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
        public int SIZE
        {
            get { return size; }
            set { size = value; }
        }
        public void Reset()
        {
            hp = 0;
            mp = 0;
            pd = 0;
            md = 0;
            pr = 0;
            mr = 0;
            x = 0;
            y = 0;
            size = 0;
        }
        public byte[] GetBytes()
        {
            byte[] Bytes = new Byte[32];
            int i, count = 0;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(hp >> ((3 - i) << 3));
            count++;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(mp >> ((3 - i) << 3));
            count++;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(pd >> ((3 - i) << 3));
            count++;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(md >> ((3 - i) << 3));
            count++;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(pr >> ((3 - i) << 3));
            count++;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(mr >> ((3 - i) << 3));
            count++;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(x >> ((3 - i) << 3));
            count++;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(y >> ((3 - i) << 3));
            count++;
            for (i = 0; i < 4; i++)
                Bytes[i + 4 * count] = (Byte)(size >> ((3 - i) << 3));
            count++;
            return Bytes;
        }
        public void SetBytes(byte[] Bytes)
        {
            Reset();
            int i, count = 0;
            for (i = 0; i < 4; i++)
                hp += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
            for (i = 0; i < 4; i++)
                mp += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
            for (i = 0; i < 4; i++)
                pd += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
            for (i = 0; i < 4; i++)
                md += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
            for (i = 0; i < 4; i++)
                pr += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
            for (i = 0; i < 4; i++)
                mr += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
            for (i = 0; i < 4; i++)
                x += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
            for (i = 0; i < 4; i++)
                y += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
            for (i = 0; i < 4; i++)
                size += Bytes[i + 4 * count] << ((3 - i) << 3);
            count++;
        }
    }
}
