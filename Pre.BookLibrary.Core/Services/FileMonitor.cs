using Pre.BookLibrary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre.BookLibrary.Core.Services
{
    public class FileMonitor
    {
        public void OnFileWritten(object sender, FileAccessEventArgs e)
        {
            LogToFile(e);
        }

        public void OnFileOpened(object sender, FileAccessEventArgs e)
        {
            LogToFile(e);
        }

        public void OnFileRead(object sender, FileAccessEventArgs e)
        {
            Console.WriteLine($"File read: {e.FilePath}");
        }

        private void LogToFile(FileAccessEventArgs e)
        {
            string logPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName, 
                                          "assets", 
                                          "fileAccessLog.txt");
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{e.Timestamp}: {e.Filename} accessed by {e.Username}");
            }
        }
    }

}
