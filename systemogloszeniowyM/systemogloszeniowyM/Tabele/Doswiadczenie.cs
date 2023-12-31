﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace systemogloszeniowyM.Tabele
{
    public class Doswiadczenie
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int IdUzytkownika {  get; set; }
        public string Stanowisko {  get; set; }
        public string NazwaFirmy { get; set; }
        public string Lokalizacja {  get; set; }
        public string OkresZatrudnieniaOd {  get; set; }
        public string OkresZatrudnieniaDo { get; set; }
        public string Obowiazki {  get; set; }  
    }
}
