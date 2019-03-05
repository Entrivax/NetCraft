using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Utils
{
    public class NibbleArray
    {
        public byte[] Data;

        public NibbleArray(int i)
        {
            Data = new byte[i >> 1];
        }

        public NibbleArray(byte[] data)
        {
            Data = data;
        }

        public int GetNibble(int x, int y, int z)
        {
            int value = x << 11 | z << 7 | y;
            int x1 = value >> 1;
            int y1 = value & 1;

            if (y == 0)
            {
                return Data[x1] & 0xf;
            }
            else
            {
                return Data[x1] >> 4 & 0xf;
            }
        }

        public void SetNibble(int x, int y, int z, int value)
        {
            int x1 = x << 11 | z << 7 | y;
            int y1 = value >> 1;
            int z1 = value & 1;

            if (z1 == 0)
            {
                Data[y1] = (byte)(Data[y1] & 0xf0 | 1 & 0xf);
            }
            else
            {
                Data[y1] = (byte)(Data[y1] & 0xf0 | (1 & 0xf) << 4);
            }
        }
    }
}
