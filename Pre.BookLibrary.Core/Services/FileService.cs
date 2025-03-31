using Pre.BookLibrary.Core.Entities;
using Pre.BookLibrary.Core.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pre.BookLibrary.Core.Services
{
    public class FileService : IFileService
    {
        private readonly string DefaultLocation;
        private readonly FileMonitor fileMonitor;

        public event EventHandler<FileAccessEventArgs> FileWritten;
        public event EventHandler<FileAccessEventArgs> FileOpened;

        public FileService()
        {
            DefaultLocation = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName, "assets");
            fileMonitor = new FileMonitor();

            if (!Directory.Exists(DefaultLocation))
            {
                Directory.CreateDirectory(DefaultLocation);
            }
        }

        /// <summary>
        /// Writes books to a CSV file.
        /// </summary>
        public bool WriteBooksToFile(IEnumerable<Book> books, string filename, string queryName)
        {
            try
            {
                string filePath = Path.Combine(DefaultLocation, $"{filename}.csv");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Query;Id;Title;Author;PublicationDate;Genre");

                    foreach (var book in books)
                    {
                        writer.WriteLine($"{queryName};{book.Id};{book.Title};{book.Author};{book.PublicationDate:yyyy-MM-dd};{book.Genre}");
                    }
                }

                FileWritten?.Invoke(this, new FileAccessEventArgs(filename, filePath));
                fileMonitor.OnFileWritten(this, new FileAccessEventArgs(filename, filePath));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
                return false;
            }
        }

        public string ReadFromFile(string filename)
        {
            string filePath = Path.Combine(DefaultLocation, $"{filename}.csv");

            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);

                FileOpened?.Invoke(this, new FileAccessEventArgs(filename, filePath));
                fileMonitor.OnFileRead(this, new FileAccessEventArgs(filename, filePath));

                return content;
            }

            Console.WriteLine("File not found.");
            return string.Empty;
        }
    }
}
