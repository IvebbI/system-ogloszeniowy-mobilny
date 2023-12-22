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
            _database = new SQLiteAsyncConnection(dbPath);
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
        public async Task<int> StworzUzytkownika(Uzytkownik uzytkownik)
        {
            if (await CzyEmailJuzIstnieje(uzytkownik.Email))
            { 
                return -1;
            }
            return await _database.InsertAsync(uzytkownik);
        }

        public async Task<bool> CzyEmailJuzIstnieje(string email)
        {
            var existingUser = await _database.Table<Uzytkownik>().FirstOrDefaultAsync(u => u.Email == email);
            return existingUser != null;
        }
        public async Task<bool> CzyEmailJuzIstniejeF(string email)
        {
            var existingUser = await _database.Table<Firma>().FirstOrDefaultAsync(u => u.email == email);
            return existingUser != null;
        }
        public async Task<Uzytkownik> ZalogujUzytkownika(string email, string haslo)
        {
            return await _database.Table<Uzytkownik>().FirstOrDefaultAsync(u => u.Email == email && u.Haslo == haslo);
        }
        public async Task<int> StworzFirme(Firma firma)
        {
            if (await CzyEmailJuzIstniejeF(firma.email))
            {
                return -1;
            }
            return await _database.InsertAsync(firma);
        }


        public async Task<Firma> ZalogujFirme(string email, string haslo)
        {
            return await _database.Table<Firma>().FirstOrDefaultAsync(u => u.email == email && u.haslo == haslo);
        }


    }
}
