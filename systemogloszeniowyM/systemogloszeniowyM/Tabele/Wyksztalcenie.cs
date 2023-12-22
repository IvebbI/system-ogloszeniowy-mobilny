using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Wyksztalcenie
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; } 
        public int IdUzytkownika {  get; set; }
        public string NazwaSzkoly { get; set; }
        public string Miejscowosc { get; set; }
        public string PoziomWyksztalcenia {  get; set; }
        public string Kierunek { get; set; }
        public DateTime UczestniczylOd {  get; set; }
        public DateTime UczestniczylDo {  get; set; }

    }
}
