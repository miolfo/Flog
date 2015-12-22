using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

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

        //Variables for using timed logging
        TEntry _timerCallback;
        private Timer _timer;
        private bool _usingTimedLogging = false;

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
            //If log type is write and the log already exists, throw exception
            if (File.Exists(path + "/" + fileName) && type == LogType.LOGTYPE_WRITE) throw new Exception("Can't write to already existing log file!"); 
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

        public delegate T TEntry();

        /// <summary>
        /// A function that can be used to set the logger to write a new entry at defined intervals
        /// </summary>
        /// <param name="ms">Time between each consecutive log entry in ms</param>
        /// <param name="entryCallback">A callback function that returns an entry, which is added to the log every ms milliseconds</param>
        public void TimedEntry(int ms, TEntry entryCallback)
        {
            if (_usingTimedLogging)
            {
                Console.WriteLine("Timed logging already set! Stop logger before restarting");
                return;
            }
            else
            {
                _usingTimedLogging = true;
            }
            _timerCallback = entryCallback;
            _timer = new Timer(ms);
            _timer.Elapsed += TimedLog;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        /// <summary>
        /// Stop the timed logging
        /// </summary>
        public void StopTimedLogging()
        {
            _timer.Enabled = false;
            _usingTimedLogging = false;
        }

        public List<T> GetEntries()
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

        private void TimedLog(Object source, ElapsedEventArgs e)
        {
            Log(_timerCallback());
            Console.WriteLine("Added entry");
        }

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
