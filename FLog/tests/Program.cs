﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLog
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileAccesser writer = new FileAccesser("juu/", "asd.txt");
            //writer.AppendFile("joopati joo");
            /*String[] content = writer.GetFileContents();
            foreach (String s in content)
            {
                Console.WriteLine(s);
            }*/

            /*FastLog logger = new FastLog("logTests", "testlog.flog", LogType.LOGTYPE_WRITE);
            for (int i = 0; i < 123; i++)
            {
                SimpleEntry e2 = new SimpleEntry(i, "joo");
                logger.Log(e2);                
            }*/
            FastLog logger2 = new FastLog("logTests", "testlog.flog", LogType.LOGTYPE_READ);
            Console.WriteLine(logger2.GetEntryWithId(32).content);
        }
    }
}
