using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLog
{
    class SimpleEntry : AbstractLogEntry
    {
        public override int entryID
        {
            get;
            set;
        }

        public override DateTime timeStamp
        {
            get;
            set;
        }
        public override string content
        {
            get;
            set;
        }

        public SimpleEntry(int entryID, String content) : base(entryID, content) { }

    }
}
