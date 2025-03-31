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

        Console.WriteLine("All books ordered by id");
        PrintBooks(bookService.GetAllBooks());
        /* End example */

        Console.WriteLine("Book by ID 1:");
        PrintBook(bookService.GetBookById(1));

        Console.WriteLine($"Total Books in Database: {bookService.CountBooksInDatabase()}");

        Console.WriteLine("Books Sorted by Author and Date:");
        PrintBooks(bookService.GetBooksSortedByAuthorAndDate());

        Console.WriteLine("Fiction Books:");
        PrintBooks(bookService.GetBooksByGenre("Fiction"));

        Console.WriteLine("Books by Year 2022 and Author 'John Doe':");
        PrintBooks(bookService.GetBooksByYearAndAuthor(2022, "John Doe"));

        Console.WriteLine("Books by Author 'Jane Doe' and Genre 'Mystery':");
        PrintBooks(bookService.GetBooksByAuthorAndGenre("Jane Doe", "Mystery"));

        Console.WriteLine("Number of Books Grouped by Author:");
        foreach (var group in bookService.NumberOfBooksGroupedByAuthor())
        {
            Console.WriteLine($"Author: {group.Key}, Books: {group.Count()}");
        }

        Console.WriteLine("Book Titles:");
        foreach (string title in bookService.GetBookTitles())
        {
            Console.WriteLine(title);
        }

        Console.WriteLine("Check if Books from Author 'John Doe' Exist:");
        Console.WriteLine(bookService.CheckIfBooksFromAuthorExists("John Doe"));

        Console.WriteLine("Last 5 Published Books:");
        PrintBooks(bookService.GetLastFivePublished());

        Console.WriteLine("Unique Genres:");
        foreach (var genre in bookService.GetUniqueGenres())
        {
            Console.WriteLine(genre);
        }

        Console.WriteLine($"Average Publication Year: {bookService.GetAveragePublicationYear():F2}");

        Console.WriteLine($"Author with Most Books: {bookService.GetAuthorWithMostBooks()}");

        Console.WriteLine($"Average Number of Books per Author: {bookService.GetAverageNumberOfBooksPerAuthor()}");

        Console.WriteLine("Books with the Longest Title:");
        PrintBooks(bookService.GetBooksWithLongestTitle());

        /* 2. Streams */
        IEnumerable<Book> booksByGenre = bookService.GetBooksByGenre("Fiction");
        fileService.WriteBooksToFile(booksByGenre, "export", "GetBooksByGenre");
        Console.WriteLine(fileService.ReadFromFile("export"));
        IEnumerable<Book> booksByAuthorAndGenre = bookService.GetBooksByAuthorAndGenre("Kurt Vonnegut", "Science Fiction");
        fileService.WriteBooksToFile(booksByAuthorAndGenre, "export", "GetBooksByAuthorAndGenre");
        Console.WriteLine(fileService.ReadFromFile("export"));
        IEnumerable<Book> booksByYearAndAuthor = bookService.GetBooksByYearAndAuthor(1969, "Kurt Vonnegut");
        fileService.WriteBooksToFile(booksByYearAndAuthor, "export", "GetBooksByYearAndAuthor");
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
