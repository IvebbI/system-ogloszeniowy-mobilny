using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Jezyk
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int IdUzytkownika {  get; set; }
        public string NazwaJezyka { get; set; }
        public string Poziom { get; set; }
    }
}
