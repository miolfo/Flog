using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace FLog
{
    class FileAccesser : IFileAccesser
    {
        private readonly String _fileName;
        private readonly String _filePath;
        private StreamWriter _sw;
        public String fileName
        {
            get
            {
                return _fileName;
            }
        }

        public String filePath
        {
            get
            {
                return _filePath;
            }
        }

        public FileAccesser(String filePath, String fileName)
        {
            _filePath = filePath;
            if(_filePath[_filePath.Length-1] != '/' ||  _filePath[_filePath.Length-1] != '\\')
                _filePath += '/';
            _fileName = fileName;
            if (!File.Exists(_filePath + _fileName)) CreateFile();
            _sw = File.AppendText(_filePath + _fileName);
            _sw.AutoFlush = true;
        }

        public bool CreateFile()
        {
            String fp = _filePath + _fileName;
            if (File.Exists(fp))
            {
                Console.WriteLine("File " + fp + " already exists!");
                return false;
            }
            else
            {
                if(!Directory.Exists(_filePath))
                    Directory.CreateDirectory(_filePath);

                File.Create(fp).Close();
                return true;
            }
        }

        public bool AppendFile(String data)
        {
            String fp = _filePath + _fileName;
            if (!File.Exists(fp))
            {
                Console.WriteLine("File " + fp + " does not exist! Create the file first.");
                return false;
            }
            else
            {
                _sw.WriteLine(data);
                return true;
            }
        }

        public String[] GetFileContents()
        {
            String fp = _filePath + _fileName;
            if (!File.Exists(fp))
            {
                Console.WriteLine("Can't read file " + fp + " since it does not exist!");
                return new String[0];
            }
            else
            {
                _sw.Close();
                String[] contents = File.ReadAllLines(fp);
                _sw = File.AppendText(fp);
                return contents;
            }
        }

    }
}
