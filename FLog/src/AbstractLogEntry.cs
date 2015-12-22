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
        public int entryID
        {
            get;
            set;
        }
        /// <summary>
        /// Timestamp of the log entry
        /// </summary>
        public DateTime timeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// Rest of the content for the entry
        /// </summary>
        public String content
        {
            get;
            set;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AbstractLogEntry()
            : this(0, "")
        {
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
        public abstract String ToString();

        /// <summary>
        /// Parse a string to the entry format
        /// </summary>
        /// <param name="entryString">Entry in the string form</param>
        public abstract void InitFromString(string entryString);
    }
}
