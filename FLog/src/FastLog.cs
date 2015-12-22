using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLog
{
    enum LogType { LOGTYPE_READ, LOGTYPE_WRITE};

    class FastLog<T> where T : AbstractLogEntry 
    {
        private List<T> _entries = new List<T>();
        private IFileAccesser _logAccess;
        private int _maxLogEntries = 10000;
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
        public void Log(T entry)
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

        public IEnumerable<T> GetEntries()
        {
            return _entries;
        }

        public T GetEntryWithId(int id)
        {
            foreach(T entry in _entries)
            {
                if (entry.entryID == id) return entry;
            }
            throw new Exception("No entry found with id " + id);
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
            //TODO: If the log file is split to multiple files, read them all
            string[] logFileLines = _logAccess.GetFileContents();
            foreach (string line in logFileLines)
            {
                T entry = (T)Activator.CreateInstance(typeof(T), new object[] { });
                entry.InitFromString(line);
                _entries.Add(entry);
            }
        }
        #endregion
    }
}
