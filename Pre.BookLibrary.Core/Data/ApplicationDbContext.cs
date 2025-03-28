using System;
using Microsoft.EntityFrameworkCore;
using Pre.BookLibrary.Core.models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Pre.BookLibrary.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public ApplicationDbContext()
    {
            // Drop and recreate the database on start
            Database.EnsureDeleted();
            Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionstring = "Server=.\\SQLEXPRESS;Database=MoviesDB;Trusted_Connection=True;TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(connectionstring);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var books = new Book[]
         {
             new Book { Id = 1, Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", PublicationDate = new DateTime(1997, 6, 26), Genre = "Fantasy" },
             new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", Author = "J.K. Rowling", PublicationDate = new DateTime(1998, 7, 2), Genre = "Fantasy" },
             new Book { Id = 3, Title = "A Game of Thrones", Author = "George R.R. Martin", PublicationDate = new DateTime(1996, 8, 6), Genre = "Fantasy" },
             new Book { Id = 4, Title = "A Clash of Kings", Author = "George R.R. Martin", PublicationDate = new DateTime(1998, 11, 16), Genre = "Fantasy" },
             new Book { Id = 5, Title = "The Hobbit", Author = "J.R.R. Tolkien", PublicationDate = new DateTime(1937, 9, 21), Genre = "Fantasy" },
             new Book { Id = 6, Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", PublicationDate = new DateTime(1954, 7, 29), Genre = "Fantasy" },
             new Book { Id = 7, Title = "Murder on the Orient Express", Author = "Agatha Christie", PublicationDate = new DateTime(1934, 1, 1), Genre = "Mystery" },
             new Book { Id = 8, Title = "The Shining", Author = "Stephen King", PublicationDate = new DateTime(1977, 1, 28), Genre = "Horror" },
             new Book { Id = 9, Title = "Foundation", Author = "Isaac Asimov", PublicationDate = new DateTime(1951, 5, 1), Genre = "Science Fiction" },
             new Book { Id = 10, Title = "Sherlock Holmes", Author = "Arthur Conan Doyle", PublicationDate = new DateTime(1887, 11, 1), Genre = "Mystery" },
             new Book { Id = 11, Title = "Adventures of Huckleberry Finn", Author = "Mark Twain", PublicationDate = new DateTime(1884, 12, 10), Genre = "Adventure" },
             new Book { Id = 12, Title = "The Old Man and the Sea", Author = "Ernest Hemingway", PublicationDate = new DateTime(1952, 9, 1), Genre = "Fiction" },
             new Book { Id = 13, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublicationDate = new DateTime(1925, 4, 10), Genre = "Fiction" },
             new Book { Id = 14, Title = "Pride and Prejudice", Author = "Jane Austen", PublicationDate = new DateTime(1813, 1, 28), Genre = "Romance" },
             new Book { Id = 15, Title = "A Tale of Two Cities", Author = "Charles Dickens", PublicationDate = new DateTime(1859, 4, 30), Genre = "Historical" },
             new Book { Id = 16, Title = "War and Peace", Author = "Leo Tolstoy", PublicationDate = new DateTime(1869, 1, 1), Genre = "Historical" },
             new Book { Id = 17, Title = "The Time Machine", Author = "H.G. Wells", PublicationDate = new DateTime(1895, 5, 7), Genre = "Science Fiction" },
             new Book { Id = 18, Title = "The Raven", Author = "Edgar Allan Poe", PublicationDate = new DateTime(1845, 1, 29), Genre = "Poetry" },
             new Book { Id = 19, Title = "The Da Vinci Code", Author = "Dan Brown", PublicationDate = new DateTime(2003, 3, 18), Genre = "Thriller" },
             new Book { Id = 20, Title = "Angels & Demons", Author = "Dan Brown", PublicationDate = new DateTime(2000, 5, 1), Genre = "Thriller" },
             new Book { Id = 21, Title = "The Catcher in the Rye", Author = "J.D. Salinger", PublicationDate = new DateTime(1951, 7, 16), Genre = "Fiction" },
             new Book { Id = 22, Title = "To Kill a Mockingbird", Author = "Harper Lee", PublicationDate = new DateTime(1960, 7, 11), Genre = "Fiction" },
             new Book { Id = 23, Title = "1984", Author = "George Orwell", PublicationDate = new DateTime(1949, 6, 8), Genre = "Dystopian" },
             new Book { Id = 24, Title = "Animal Farm", Author = "George Orwell", PublicationDate = new DateTime(1945, 8, 17), Genre = "Satire" },
             new Book { Id = 25, Title = "Brave New World", Author = "Aldous Huxley", PublicationDate = new DateTime(1932, 8, 1), Genre = "Dystopian" },
             new Book { Id = 26, Title = "Fahrenheit 451", Author = "Ray Bradbury", PublicationDate = new DateTime(1953, 10, 19), Genre = "Dystopian" },
             new Book { Id = 27, Title = "Twenty Thousand Leagues Under the Sea", Author = "Jules Verne", PublicationDate = new DateTime(1870, 6, 20), Genre = "Science Fiction" },
             new Book { Id = 28, Title = "Moby-Dick", Author = "Herman Melville", PublicationDate = new DateTime(1851, 10, 18), Genre = "Adventure" },
             new Book { Id = 29, Title = "Frankenstein", Author = "Mary Shelley", PublicationDate = new DateTime(1818, 1, 1), Genre = "Horror" },
             new Book { Id = 30, Title = "Dracula", Author = "Bram Stoker", PublicationDate = new DateTime(1897, 5, 26), Genre = "Horror" },
             new Book { Id = 31, Title = "2001: A Space Odyssey", Author = "Arthur C. Clarke", PublicationDate = new DateTime(1968, 7, 1), Genre = "Science Fiction" },
             new Book { Id = 32, Title = "Do Androids Dream of Electric Sheep?", Author = "Philip K. Dick", PublicationDate = new DateTime(1968, 3, 1), Genre = "Science Fiction" },
             new Book { Id = 33, Title = "Slaughterhouse-Five", Author = "Kurt Vonnegut", PublicationDate = new DateTime(1969, 3, 31), Genre = "Science Fiction" },
             new Book { Id = 34, Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", PublicationDate = new DateTime(1979, 10, 12), Genre = "Science Fiction" },
             new Book { Id = 35, Title = "Good Omens", Author = "Terry Pratchett", PublicationDate = new DateTime(1990, 5, 1), Genre = "Fantasy" },
             new Book { Id = 36, Title = "American Gods", Author = "Neil Gaiman", PublicationDate = new DateTime(2001, 6, 19), Genre = "Fantasy" },
             new Book { Id = 37, Title = "The Handmaid's Tale", Author = "Margaret Atwood", PublicationDate = new DateTime(1985, 4, 1), Genre = "Dystopian" },
             new Book { Id = 38, Title = "The Left Hand of Darkness", Author = "Ursula K. Le Guin", PublicationDate = new DateTime(1969, 3, 1), Genre = "Science Fiction" },
             new Book { Id = 39, Title = "Neverwhere", Author = "Neil Gaiman", PublicationDate = new DateTime(1996, 9, 16), Genre = "Fantasy" },
             new Book { Id = 40, Title = "Oryx and Crake", Author = "Margaret Atwood", PublicationDate = new DateTime(2003, 5, 6), Genre = "Science Fiction" },
             new Book { Id = 41, Title = "The Dispossessed", Author = "Ursula K. Le Guin", PublicationDate = new DateTime(1974, 5, 1), Genre = "Science Fiction" },
         };
         modelBuilder.Entity<Book>().HasData(books);
    }
}
