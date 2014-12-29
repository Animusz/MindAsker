using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindAsker
{
    /**
     * Funkcje interfajsu mają na celu generowanie poleceń SQL. 
     * Tak żeby potem podkładać tylko te zapytania w odpowiednie miejsca.
     * Funkcje odnoszą się do pojedyńczego rekordu/wpisu tabeli.
     * Nazwy funkcji chba same siebie komenują, takż za bardzo się nad nimi nie będę rozpisywał.
     **/
    interface Record
    {
        String editTemplateSQL();
        /**
         * Zmienna bool bState, będzie miała na celu sprawdzanie czy wyszukujemy 
         * uwzględniając stan wpisu.
         **/
        String findTemplateSQL(bool bState);
        String removeTemplateSQL();
        String saveTemplateSQL();
        String existTemplateSQL();
    }
}
