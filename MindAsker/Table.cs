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
     * Funkcje odnoszą sie do tabeli bazy danych.
     * Nazwy funkcji chba same siebie komenują, takż za bardzo się nad nimi nie będę rozpisywał.
     **/
    interface Table
    {
        string createTableTemplateSQL();
    }
}
