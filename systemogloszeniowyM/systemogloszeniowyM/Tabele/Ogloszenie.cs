using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Ogloszenie
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int Idfirmy { get; set; }
        public string Nazwa { get; set; }
        public string PoziomStanowiska { get; set; }
        public string RodzajUmowy { get; set; }
        public string WymiarEtatu { get; set; }
        public string RodzajPracy { get; set; }
        public string Wynagrodzenie { get;set; }
        public DateTime DniPracy { get; set; }
        public DateTime Godzinypracy { get; set; }
        public DateTime DataWaznosci { get; set; }
        public string Kategoria { get; set; }
        public string ZakresObowiazkow { get; set; }
        public string OferowaneBenefity { get; set; }
        public string Wymagania { get; set; }
        public string Informacje { get; set; }
    }
}
