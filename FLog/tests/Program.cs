using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLog
{
    class Program
    {
        public static SimpleEntry asd()
        {
            SimpleEntry e = new SimpleEntry(0, "seppo hovi");
            return e;            
        }

        static void Main(string[] args)
        {
            /*FastLog<SimpleEntry> logger = new FastLog<SimpleEntry>("logTests", "testlog.flog", LogType.LOGTYPE_WRITE);
            for (int i = 0; i < 123; i++)
            {
                SimpleEntry e2 = new SimpleEntry(i, "joo");
                logger.Log(e2);                
            }*/
            /*FastLog<SimpleEntry> logger2 = new FastLog<SimpleEntry>("logTests", "testlog.flog", LogType.LOGTYPE_READ);
            Console.WriteLine(logger2.GetEntryWithId(32).content);*/
            FastLog<SimpleEntry> timedLogger = new FastLog<SimpleEntry>("logTests", "timedlog2.flog", LogType.LOGTYPE_WRITE);
            FastLog<SimpleEntry>.TEntry cb = asd;
            timedLogger.TimedEntry(1000, cb);

            while (timedLogger.GetEntries().Count < 5) {

            }
        }
    }
}
