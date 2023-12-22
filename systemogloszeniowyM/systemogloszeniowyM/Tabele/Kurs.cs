using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Kurs
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int IdUzytkownika { get; set; }
        public string NazwaSzkolenia { get; set; }
        public string Organizator { get;set; }
        public DateTime Data { get; set; }    
    }
}
