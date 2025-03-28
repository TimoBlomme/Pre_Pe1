using Pre.BookLibrary.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre.BookLibrary.Core.Services
{
    interface IFileService
    {
        /*Opgave streams*/
        /*
         1. Deze methode ontvangt een lijst van books en schrijft elk book weg naar een file (filename). 
            Plaats telkens de naam van de query (bijv. GetBooksByYearAndAuthor) in de eerste kolom gevolgd 
            door de informatie van 1 book op elke volgende kolom, 
            gescheiden door een puntkomma ; het bestand sla je op met de extensie .csv
         */
        bool WriteBooksToFile(IEnumerable<Book> books, string filename, string queryName);
        /*
         2. Werk de methode ReadFromFile uit. Deze methode opent een bestand in de DefaultLocation
            en leest de inhoud uit in een string. 
         */
        string ReadFromFile(string filename);

        /*Opgave Events*/
        /*
         -Een event dat getriggerd wordt wanneer er weggeschreven wordt in een file.
         -Een event dat getriggerd wordt wanneer een file wordt geopend. 
         -Maak gebruik van een eigen EventArgs klasse die onderstaande zaken bevat:
                *Filename van het bestand dat gewijzigd of uitgelezen wordt.
                *Een timestamp met de datum en tijd waarop het bestand benadert wordt.
                *De gebruikersnaam van de ingelogde gebruiker.
         -Implementeer een klasse FileMonitor die de twee gevraagde eventhandlers voorziet
         -Koppel de eventhandlers aan de event in de FileService klasse
         -Elke eventHandler zal een logbestand fileAccessLog.txt openen en op een nieuwe regel de filename, gebruikersnaam en timestamp wegschrijven.
         -De fileAccessLog wordt aangemaakt en opgeslagen in de Assets folder van de applicatie.
         -Test de eventhandlers uit in de program.cs door een query weg te schrijven in de export.csv en door de inhoud van export.csv op te halen.
         */
    }
}
