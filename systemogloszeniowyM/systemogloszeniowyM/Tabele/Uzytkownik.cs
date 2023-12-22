using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Uzytkownik
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public int IsAdmin { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string DataUrodzenia { get; set; }
        public string Telefon { get; set; }
        public string LinkDoZdjecia { get; set; }
        public string Adres { get; set; }
        public string StanowiskoPracy { get; set; }
        public string OpisPracy { get; set; }
        public string PodsumowanieZawodowe { get; set; }
        public string GithubProfil { get; set; }
    }
}
