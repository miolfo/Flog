using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLog
{
    /// <summary>
    /// Interface for classes that can be used for accessing files
    /// </summary>
    interface IFileAccesser
    {
        /// <summary>
        /// Relative path of the file
        /// </summary>
        String filePath
        {
            get;
        }
        /// <summary>
        /// Name of the handled file
        /// </summary>
        String fileName
        {
            get;
        }

        /// <summary>
        /// Append to the content of the file
        /// </summary>
        /// <param name="data">Data to be written to file</param>
        /// <returns>return true if success</returns>
        bool AppendFile(string data);
        /// <summary>
        /// Create a new file
        /// </summary>
        /// <returns>return true on success</returns>
        bool CreateFile();

        /// <summary>
        /// Get the contents of the file in array, where each line is stored in the array
        /// </summary>
        /// <returns>Array of lines in the file</returns>
        String[] GetFileContents();
    }
}
