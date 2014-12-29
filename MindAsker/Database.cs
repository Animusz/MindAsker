using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindAsker
{
    /**
     * Interfejs odpowiedzialny za bazę danych.
     * Jeśli funkcje natrafią na jakiś błąd bazy, zapytania, lub danych wejściowych 
     * wyrzucą błąd lub wynikiem będzie 0.
     **/
    interface Database
    {
        bool connect();

        bool disconnect();

        /**
         * Zapisujemy obiekt który dziedziczy po interfejsie Record.
         * Zwracany jest numer ID wstawionego wiersza.
         **/
        long save(MindAsker.Record rec);

        /**
         * Usuwamy obiekt który dziedziczy po interfejsie Record.
         * Jeśli wpis zostanie usunięty zwracane jest true.
         **/
        bool remove(MindAsker.Record rec);

        /**
         * Edytujemy obiekt który dziedziczy po interfejsie Record.
         * Zwracany jest zaktualizowany rekord
         **/
        MindAsker.Event edit(MindAsker.Record rec);

        /**
         * Wyszukujemy obiekty które są podobne do obiektu dziedziczącego po interfejsie Record.
         * MindAsker.Record rec - jest to szablon według którego wyszukujemy podobne elementy.
         * Nie wszystkie jego pola muszą być wypełnione.
         * bool bState - ma na celu sprawdzanie czy wyszukujemy uwzględniając odpowiedni stan rekordu
         * Zwracana jest lista obiektów pasujących do szablonu.
         **/
        List<MindAsker.Event> find(MindAsker.Record rec, bool bState);
    }
}
