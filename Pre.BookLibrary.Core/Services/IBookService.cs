using Pre.BookLibrary.Core.Data;
using Pre.BookLibrary.Core.models;

namespace Pre.BookLibrary.Core.Services
{
    public interface IBookService
    {
        /*
         Voorbeeld: Haal alle boeken uit de database
         */
        IEnumerable<Book> GetAllBooks();

        /*Opgave Linq*/
        /*
         1. Haal een book uit de database voor een gegeven Id
         */
        Book GetBookById(int id); 
        /*
         2. Tel het aantal boeken in de database
         */
        int CountBooksInDatabase();
        /*
         3. Haal alle boeken op oplopend gesorteerd volgens author en datum 
         */
        IEnumerable<Book> GetBooksSortedByAuthorAndDate();
        /*
         4. Haal alle boeken op oplopend gesorteerd volgens een gegeven genre 
         */
        IEnumerable<Book> GetBooksByGenre(string genre);
        /*
         5. Haal alle boeken op gesorteerd op titel voor een gegeven year en author
         */
        IEnumerable<Book> GetBooksByYearAndAuthor(int year, string author);
        /*
         6. Haal alle boeken op gesorteerd op titel volgens author en genre 
         */
        IEnumerable<Book> GetBooksByAuthorAndGenre(string author, string genre);
        /*
         7. Haal per auteur het aantal books op. 
         */
        List<IGrouping<string, Book>> NumberOfBooksGroupedByAuthor();
        /*
         8. Haal alle titels op. 
         */
        IEnumerable<Book> GetBookTitles();
        /*
         9. Controleer of er books bestaan voor een gegeven author. 
         */
        bool CheckIfBooksFromAuthorExists(string author);
        /*
         10. Haal de 5 recentste books op. 
         */
        IEnumerable<Book> GetLastFivePublished();
        /*
         11. Haal alle unieke genres op. 
         */
        IEnumerable<string> GetUniqueGenres();
        /*
         12. Haal het gemiddelde jaar van publicatie op. 
         */
        double GetAveragePublicationYear();
        /*
         13. Haal de author op die het meeste boeken schreef. 
         */
        string GetAuthorWithMostBooks();
        /*
         14. Haal de authors op met hun gemiddelde aantal boeken. 
         */
        string GetAverageNumberOfBooksPerAuthor();
        /*
         15. Haal de boeken op met de langste titel. 
         */
        IEnumerable<Book> GetBooksWithLongestTitle();
    }
}