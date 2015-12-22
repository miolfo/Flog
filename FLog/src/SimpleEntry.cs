using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLog
{
    class SimpleEntry : AbstractLogEntry
    {

        //Simple entry class does not need to override the default constructors written for abstract base class
        public SimpleEntry()
            : base()
        {
        }

        public SimpleEntry(int entryID, string content)
            : base(entryID, content)
        {
        }

        public SimpleEntry(int entryID, string content, DateTime timeStamp)
            : base(entryID, content, timeStamp)
        {
        }

        public override String ToString()
        {
            String s = "";
            String time = "[" + timeStamp.ToString() + "]";
            String id = "[" + entryID.ToString() + "]";
            s = time + id + content;
            return s;
        }

        public override void InitFromString(string entryString)
        {
            string e = entryString;

            //Get the timestamp from between the first set of brackets
            e = e.Remove(0, 1);
            int bracketIndex = e.IndexOf(']');
            string timeStampString = e.Substring(0, bracketIndex);
            e = e.Remove(0, bracketIndex+1);
            timeStamp = DateTime.Parse(timeStampString);

            //Get the id from between the second set of brackets
            e = e.Remove(0, 1);
            bracketIndex = e.IndexOf(']');
            string idString = e.Substring(0, bracketIndex);
            e = e.Remove(0, bracketIndex+1);
            entryID = int.Parse(idString);

            //Remaining is the content
            content = e;
        }
    }
}
