using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Aplikacja
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get;set; }
        public int IdUzytkownika { get; set; }
        public int IdOgloszenia { get; set; }
        public string StatusAplikacje { get; set; }
    }
}
