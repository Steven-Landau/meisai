using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meisai.Tools
{
    public static class RandomGen
    {
        static Random random;

        public static void Initiate()
        {
            long tick = DateTime.Now.Ticks;
            random = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        }
        public static double getDouble()
        {
            return random.NextDouble();
        }
    }
}
