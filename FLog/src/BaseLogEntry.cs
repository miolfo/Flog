using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLog
{
    /// <summary>
    /// Interface for representing a single entry in the log
    /// </summary>
    class BaseLogEntry : AbstractEntry
    {
        /// <summary>
        /// Identifier for an entry
        /// </summary>
        public virtual int entryID
        {
            get;
            set;
        }
        /// <summary>
        /// Timestamp of the log entry
        /// </summary>
        public virtual DateTime timeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// Rest of the content for the entry
        /// </summary>
        public virtual String content
        {
            get;
            set;
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="entryID">Id of the entry</param>
        /// <param name="content">Message of the entry</param>
        public BaseLogEntry(int entryID, String content)
            : this(entryID, content, DateTime.Now)
        {
        }

        public BaseLogEntry(int entryID, String content, DateTime timeStamp)
        {
            this.timeStamp = DateTime.Now;
            this.entryID = entryID;
            this.content = content;
        }

        /// <summary>
        /// Convert the whole entry in to a string form
        /// </summary>
        /// <returns>A String representation of the entry</returns>
        public override String ToString()
        {
            String s = "";
            String time = "[" + timeStamp.ToString() + "]";
            String id = "[" + entryID.ToString() + "]";
            s = time + id + content;
            return s;
        }

        /// <summary>
        /// Parse a string to the entry format
        /// </summary>
        /// <param name="entryString">Entry in the string form</param>
        /// <returns></returns>
        public static AbstractEntry ParseEntry(string entryString)
        {
            DateTime timeStamp;
            string content;
            int id;
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
            id = int.Parse(idString);

            //Remaining is the content
            content = e;
            BaseLogEntry entry = new BaseLogEntry(id, content, timeStamp);
            return entry;
        }

    }
}
