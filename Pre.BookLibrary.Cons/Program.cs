using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pre.BookLibrary.Core.Data;
using Pre.BookLibrary.Core.models;
using Pre.BookLibrary.Core.Services;
using Pre.BookLibrary.Core.Entities;

namespace Pre.BookLibrary.Cons;

class Program
{
    static void Main(string[] args)
    {
        // Initialize database context and services
        using var applicationDbContext = new ApplicationDbContext();
        BookService bookService = new BookService(applicationDbContext);
        FileService fileService = new FileService();
        FileMonitor monitor = new FileMonitor();

        // Subscribe FileMonitor to FileService events
        fileService.FileWritten += monitor.OnFileWritten;
        fileService.FileOpened += monitor.OnFileOpened;

        /* 1. Linq */
        /* Example */
        var totalBooks = bookService.GetAllBooks();
        PrintBooks(totalBooks);
        /* End example */

        /* 2. Streams */
        var booksByGenre = bookService.GetBooksByGenre("Fiction");
        fileService.WriteBooksToFile(booksByGenre, "export", "GetBooksByGenre");
        Console.WriteLine(fileService.ReadFromFile("export"));

        /* 3. Events - Handled by FileMonitor */

        /* Helper methods */

        void PrintBook(Book book)
        {
            PrintLine();
            Console.WriteLine($"Id: {book.Id}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"PublicationDate: {book.PublicationDate:yyyy-MM-dd}");
            Console.WriteLine($"Genre: {book.Genre}");
            PrintLine();
        }
        void PrintBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                PrintBook(book);
            }
        }
        void PrintLine()
        {
            Console.WriteLine("------------------------------------");
        }
    }
}
