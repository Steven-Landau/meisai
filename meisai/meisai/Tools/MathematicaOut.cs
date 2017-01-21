using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meisai.Tools
{
    public static class MathematicaOut
    {
        //用这个函数输出一个数组
        public static bool Out<T>(String fileName, String listName, T[] list)
        {
            using (FileStream fs = new FileStream(fileName + ".txt", 
                FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(listName + "={");
                if (list.Length > 0) sw.Write("" + list[0]);
                for (int i=0; i<list.Length; i++)
                {
                    sw.Write("," + list[i]);
                }
                sw.Write("}");
                sw.Close();
                return true;
            }
        }
    }
}
