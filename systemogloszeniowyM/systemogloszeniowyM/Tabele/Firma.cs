using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Firma
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string email { get; set; }
        public string haslo { get; set; }
        public string Nazwa { get; set; }
        public string Adres { get; set; }
        public string StronaInternetowa { get; set; }
    }
}
