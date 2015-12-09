using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLog
{
    enum LogType { LOGTYPE_READ, LOGTYPE_WRITE};

    class FastLog
    {
        private List<AbstractLogEntry> _entries = new List<AbstractLogEntry>();
        private IFileAccesser _logAccess;
        private int _maxLogEntries = 1000;
        private int _logsFilled = 1;
        private string _filePath;
        private string _fileName;
        private LogType _logType;

        public int MaxLogEntries
        {
            get
            {
                return _maxLogEntries;
            }
            set
            {
                if (value > 0 && value < 100000)
                {
                    _maxLogEntries = value;
                }
                else
                {
                    throw new Exception("Invalid integer for maximum log entries!");
                }
            }
        }

        #region constructors
        /*public FastLog()
        {
            //Create a random name for the log file
            string fileName = CreateLogfileName();
            _logAccess = new FileAccesser("log", fileName);
            _filePath = "log";
            _fileName = fileName;
        }*/

        public FastLog(LogType type)
            : this("log", CreateLogfileName(), type)
        {

        }

        public FastLog(string path, LogType type) 
            : this(path, CreateLogfileName(), type)
        {

        }
        /// <summary>
        /// Constructor for a logger object
        /// </summary>
        /// <param name="path">path of the logfile</param>
        /// <param name="fileName">name of the logfile</param>
        public FastLog(string path, string fileName, LogType type)
        {
            _logAccess = new FileAccesser(path, fileName);
            _filePath = path;
            _fileName = fileName;
            _logType = type;
            if(type == LogType.LOGTYPE_READ)
            {
                ReadLogFile();
            }
        }
        #endregion

        #region public methods
        public void Log(AbstractLogEntry entry)
        {
            if (_logType == LogType.LOGTYPE_WRITE)
            {
                _logAccess.AppendFile(entry.ToString());
                _entries.Add(entry);
                if (_entries.Count == _maxLogEntries)
                {
                    _logAccess = new FileAccesser(_filePath, _logsFilled + _fileName);
                    _entries.Clear();
                    _logsFilled++;
                }
            }
            else
            {
                Console.WriteLine("Invalid log type!");
            }
        }
        #endregion

        #region private methods
        private static string CreateLogfileName()
        {
            string name = "";
            DateTime now = DateTime.Now;
            name = "_LogFile" + now.Day + "-" + now.Month + "-" + now.Year + "-" + now.Hour + "-" + now.Minute + "-" + now.Second + "-" + now.Millisecond + ".flog";
            return name;
        }

        private void ReadLogFile()
        {
            string[] logFileLines = _logAccess.GetFileContents();
            foreach (string line in logFileLines)
            {
                AbstractLogEntry entry = ParseEntry(line);
                _entries.Add(entry);
            }
        }

        /// <summary>
        /// Parse a single string line to a (simple) entry
        /// </summary>
        /// <param name="entryString">The entry in a string format</param>
        /// <returns></returns>
        private AbstractLogEntry ParseEntry(string entryString)
        {
            AbstractLogEntry entry = new SimpleEntry(0, "asd");
            Console.WriteLine(entryString);
            entryString.
            return entry;
        }
        #endregion
    }
}
