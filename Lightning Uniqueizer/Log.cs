using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightning_Uniqueizer
{
    class Log
    {
        public static void Clear()
        {
            string dir = Environment.CurrentDirectory;
            string path = dir + "\\syslog.log";
            File.WriteAllText(path, "");
        }
        public static void AddMessage(string mess)
        {
            try
            {
                string dir = Environment.CurrentDirectory;
                string path = dir + "\\syslog.log";
                StreamWriter writer = File.AppendText(path);
                writer.WriteLine("[" + DateTime.Now.ToShortDateString() + "||" + DateTime.Now.ToLongTimeString() + "]: " + mess);
                writer.Close();
            }
            catch(Exception e)
            {
                //Parallel cancer             
            }
        }
    }
}
