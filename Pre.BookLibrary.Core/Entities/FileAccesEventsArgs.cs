using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre.BookLibrary.Core.Entities
{
    public class FileAccessEventArgs : EventArgs
    {
        public string Filename { get; }
        public DateTime Timestamp { get; }
        public string Username { get; }
        public string FilePath { get; }

        public FileAccessEventArgs(string filename, string filePath)
        {
            Filename = filename;
            Timestamp = DateTime.Now;
            Username = Environment.UserName;
            FilePath = filePath;
        }
    }
}
