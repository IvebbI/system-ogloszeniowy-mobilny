using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Umiejetnosc
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int IdUzytkownika { get; set; }
        public string NazwaUmiejetnosci {  get; set; }  
    }
}
