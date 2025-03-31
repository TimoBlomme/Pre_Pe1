using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Pre.BookLibrary.Core.Data;
using Pre.BookLibrary.Core.models;

namespace Pre.BookLibrary.Core.Services;

public delegate void BookLoadedHandler(object sender, EventArgs e);
public class BookService : IBookService
{
    private ApplicationDbContext _applicationDbContext { get; set; }
    public BookService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    /*
        Voorbeeld: Haal alle boeken uit de database
    */
    public IEnumerable<Book> GetAllBooks()
    {
        return _applicationDbContext.Books.ToList();
    }

    public Book GetBookById(int id)
    {
        return _applicationDbContext.Books.FirstOrDefault(b => b.Id == id);
    }

    public int CountBooksInDatabase()
    {
        return _applicationDbContext.Books.Count();
    }

    public IEnumerable<Book> GetBooksSortedByAuthorAndDate()
    {
        return _applicationDbContext.Books.OrderBy(b => b.Author)
                                          .ThenBy(b => b.PublicationDate)
                                          .ToList();
    }

    public IEnumerable<Book> GetBooksByGenre(string genre)
    {
        return _applicationDbContext.Books.Where(b => b.Genre == genre)
                                          .OrderBy(b => b.Title)
                                          .ToList();
    }

    public IEnumerable<Book> GetBooksByYearAndAuthor(int year, string author)
    {
        return _applicationDbContext.Books.Where(b => b.PublicationDate.Year == year && b.Author == author)
                                          .OrderBy(b => b.Title)
                                          .ToList();
    }

    public IEnumerable<Book> GetBooksByAuthorAndGenre(string author, string genre)
    {
        return _applicationDbContext.Books.Where(b => b.Author == author && b.Genre == genre)
                                          .OrderBy(b => b.Title)
                                          .ToList();
    }

    public List<IGrouping<string, Book>> NumberOfBooksGroupedByAuthor()
    {
        return _applicationDbContext.Books.GroupBy(b => b.Author)
                                          .ToList();
    }

    public IEnumerable<string> GetBookTitles()
    {
        return _applicationDbContext.Books.Select(b => b.Title) 
                                          .ToList();
    }

    public bool CheckIfBooksFromAuthorExists(string author)
    {
        return _applicationDbContext.Books.Any(b => b.Author == author);
    }

    public IEnumerable<Book> GetLastFivePublished()
    {
        return _applicationDbContext.Books.OrderByDescending(b => b.PublicationDate)
                                          .Take(5)
                                          .ToList();
    }

    public IEnumerable<string> GetUniqueGenres()
    {
        return _applicationDbContext.Books.Select(b => b.Genre)
                                          .Distinct()
                                          .ToList();
    }

    public double GetAveragePublicationYear()
    {
        return _applicationDbContext.Books.Average(b => b.PublicationDate.Year);
    }

    public string GetAuthorWithMostBooks()
    {
        return _applicationDbContext.Books.GroupBy(b => b.Author)
                                          .OrderByDescending(g => g.Count())
                                          .Select(g => g.Key)
                                          .FirstOrDefault();
    }

    public string GetAverageNumberOfBooksPerAuthor()
    {
        return (_applicationDbContext.Books.Count() / 
                (double)_applicationDbContext.Books.Select(b => b.Author)
                                                   .Distinct()
                                                   .Count())
                                                   .ToString("F2");
    }

    public IEnumerable<Book> GetBooksWithLongestTitle()
    {
        int maxLength = _applicationDbContext.Books.Max(b => b.Title.Length);
        return _applicationDbContext.Books.Where(b => b.Title.Length == maxLength)
                                          .ToList();
    }
}
