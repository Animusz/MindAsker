using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindAsker
{
    class Program
    {
        static void Main(string[] args)
        {

            MindAsker.Database db = new MindAsker.DatabaseManager();
            db.connect();

            // --- Zapis
            MindAsker.Event eSave = new Event();
            eSave.Title = "Testowy tytul";
            eSave.Desc = "Testowy opis";
            DateTime saveNow = DateTime.Now;
            eSave.AddDate = saveNow;
            //eSave.ReminderDate = saveNow;
            eSave.State = MindAsker.State.DONE;
            
            //long lastSaved = db.save(eSave);
            //Console.WriteLine("ID wstawionego wiersza: " + lastSaved);


            // --- Odczyt 

            MindAsker.Event eRead = new Event();
            eRead.Id = 2;
            //eRead.Title = "Testowy tytul"; // Szuka wszystkie z tym tytułem

            List<Event> eventsList = db.find(eRead, false);
            foreach (Event tmpEvent in eventsList) // Loop through List with foreach.
            {
                Console.WriteLine("     --------------------------------     ");
                Console.WriteLine("ID: " + tmpEvent.Id);
                Console.WriteLine(tmpEvent.Title);
                Console.WriteLine(tmpEvent.Desc);
                Console.WriteLine(tmpEvent.AddDate);
                Console.WriteLine(tmpEvent.ReminderDate);
                Console.WriteLine(tmpEvent.State);
                Console.WriteLine("     --------------------------------     ");
            }
            Console.WriteLine("===========================================");


            // Edycja
            MindAsker.Event eEdit = eventsList.ElementAt(0);
            //eEdit.Id = 999; // Wyrzuca wyjatek
            eEdit.Title = "edytowany";
            eEdit.ReminderDate = null;
            eEdit.State = MindAsker.State.DONE;

            MindAsker.Event eEdited = db.edit(eEdit);

            // Usuwanie
            MindAsker.Event eRemove = new Event();
            //eRemove.Id = 4;
            //eRemove.Id = 999; // Wyrzuca wyjatek

            //Console.WriteLine(db.remove(eRemove));


            // Odczyt - sprawdzenie poprawnosci edycji i usuwania

            /*List<Event> eventsList2 = db.find(eRead, false);
            foreach (Event tmpEvent in eventsList2) // Loop through List with foreach.
            {
                Console.WriteLine("     --------------------------------     ");
                Console.WriteLine(tmpEvent.Id);
                Console.WriteLine(tmpEvent.Title);
                Console.WriteLine(tmpEvent.Desc);
                Console.WriteLine(tmpEvent.AddDate);
                Console.WriteLine(tmpEvent.ReminderDate);
                Console.WriteLine(tmpEvent.State);
                Console.WriteLine("     --------------------------------     ");
            }
            Console.WriteLine("===========================================");*/

            db.disconnect();
        }
    }
}
