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

        public byte GetNibble(int x, int y, int z)
        {
            int value = x << 11 | z << 7 | y;
            byte x1 = (byte)(value >> 1);
            byte y1 = (byte)(value & 1);

            if (y == 0)
            {
                return (byte)(Data[x1] & 0xf);
            }
            else
            {
                return (byte)(Data[x1] >> 4 & 0xf);
            }
        }

        public void SetNibble(int x, int y, int z, byte value)
        {
            int x1 = x << 11 | z << 7 | y;
            int y1 = x1 >> 1;
            int z1 = x1 & 1;

            if (z1 == 0)
            {
                Data[y1] = (byte)(Data[y1] & 0xf0 | value & 0xf);
            }
            else
            {
                Data[y1] = (byte)(Data[y1] & 0xf0 | (value & 0xf) << 4);
            }
        }
    }
}
