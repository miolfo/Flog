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
    abstract class AbstractLogEntry
    {
        /// <summary>
        /// Identifier for an entry
        /// </summary>
        public abstract int entryID
        {
            get;
            set;
        }
        /// <summary>
        /// Timestamp of the log entry
        /// </summary>
        public abstract DateTime timeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// Rest of the content for the entry
        /// </summary>
        public abstract String content
        {
            get;
            set;
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="entryID">Id of the entry</param>
        /// <param name="content">Message of the entry</param>
        public AbstractLogEntry(int entryID, String content)
            : this(entryID, content, DateTime.Now)
        {
        }

        public AbstractLogEntry(int entryID, String content, DateTime timeStamp)
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
    }
}
