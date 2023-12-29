using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
   public class Sesja
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int idUzytkownika { get; set; }
        public string TypZalogowanego {  get; set; }

    }
}
