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

        // Define Events
        public event EventHandler<FileAccessEventArgs> FileWritten;
        public event EventHandler<FileAccessEventArgs> FileOpened;

        public FileService()
        {
            DefaultLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            fileMonitor = new FileMonitor();

            // Ensure the Assets folder exists
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
                    writer.WriteLine("Query;Id;Title;Author;Genre;PublicationDate");

                    foreach (var book in books)
                    {
                        writer.WriteLine($"{queryName};{book.Id};{book.Title};{book.Author};{book.Genre};{book.PublicationDate:yyyy-MM-dd}");
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
