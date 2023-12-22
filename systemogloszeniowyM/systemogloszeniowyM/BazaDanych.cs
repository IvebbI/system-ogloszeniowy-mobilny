using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.Tabele;

namespace systemogloszeniowyM
{
    public class BazaDanych
    {
        private readonly SQLiteAsyncConnection _database;
        public BazaDanych(string dbPath)
        {
            _database=new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Aplikacja>().Wait();
            _database.CreateTableAsync<Doswiadczenie>().Wait();
            _database.CreateTableAsync<Firma>().Wait();
            _database.CreateTableAsync<Jezyk>().Wait();
            _database.CreateTableAsync<Kurs>().Wait();
            _database.CreateTableAsync<Link>().Wait();
            _database.CreateTableAsync<Ogloszenie>().Wait();
            _database.CreateTableAsync<Umiejetnosc>().Wait();
            _database.CreateTableAsync<Uzytkownik>().Wait();
            _database.CreateTableAsync<Wyksztalcenie>().Wait();
        }
        public int StworzUzytkownika(Uzytkownik uzytkownik)
        {
            return _database.InsertAsync(uzytkownik).Result;
        }
    }
}
