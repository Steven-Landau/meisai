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
                for (int i=1; i<list.Length; i++)
                {
                    sw.Write("," + list[i]);
                }
                sw.Write("}");
                sw.Close();
                return true;
            }
        }

        public static List<String> yearData = new List<string>();
        public static void AppendYearData(String data)
        {
            yearData.Add(data);
        }
        public static void OutputYearData(String fileName, String listName,
            String[] nameList)
        {
            using (FileStream fs = new FileStream(fileName + ".txt",
                FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(listName + "=");
                sw.Write(WriteToList(ParseListToStd(yearData)));
                sw.WriteLine(";");
                for (int i=0; i<nameList.Length; i++)
                {
                    sw.WriteLine("KEY" + nameList[i] + "=" + (i+1) + ";");
                }
                sw.Close();
            }
        }

        public static String WriteToList(String[] list)
        {
            String A = "{";
            if (list.Length > 0) A += list[0];
            for (int i=1; i<list.Length; i++)
            {
                A += "," + list[i];
            }
            return A + "}";
        }
        public static String[] ParseListToStd(List<String> list)
        {
            String[] arr = new String[list.Count];
            for (int i=0; i<arr.Length; i++)
            {
                arr[i] = list[i];
            }
            return arr;
        }
    }
}
