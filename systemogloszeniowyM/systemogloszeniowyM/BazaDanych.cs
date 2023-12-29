﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;

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
            _database.CreateTableAsync<Ogloszenie>().Wait();
            _database.CreateTableAsync<Umiejetnosc>().Wait();
            _database.CreateTableAsync<Uzytkownik>().Wait();
            _database.CreateTableAsync<Wyksztalcenie>().Wait();
            _database.CreateTableAsync<Sesja>().Wait();
        }
        public async Task<int> StworzUzytkownika(Uzytkownik uzytkownik)
        {
            if (await CzyEmailJuzIstnieje(uzytkownik.Email))
            { 
                return -1;
            }
            return await _database.InsertAsync(uzytkownik);
        }
        public async void ZapiszSesje(Sesja sesja)
        {
            await _database.InsertOrReplaceAsync(sesja);
        }
        public async Task<Sesja> PobierzSesje()
        {
            return await _database.Table<Sesja>()
                                  .OrderByDescending(sesja => sesja.Id)
                                  .FirstOrDefaultAsync();
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
        public  List <Doswiadczenie> PobierzDaneDoswiadczenia(Sesja sesja)
        {
            try
            {
                return  _database.Table<Doswiadczenie>().Where(item => item.IdUzytkownika == sesja.idUzytkownika).ToListAsync().Result;
            }
            catch(Exception ex)
            {
                return null;
            }

        }




        public Uzytkownik PobierzDaneUzytkownika(Sesja sesja)
        {
            try
            {
                return _database.Table<Uzytkownik>().Where(item => item.Id == sesja.idUzytkownika).FirstAsync().Result;
            }
            catch (Exception ex)
            {
                return null;
            }
   
        }
        public async Task ZapiszDaneUzytkownika(Uzytkownik uzytkownik)
        {
            try
            {
                var result = await _database.InsertOrReplaceAsync(uzytkownik);
                if (result != 0)
                {

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd", "Nie udało się zapisać danych użytkownika. Result = 0", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas zapisywania danych użytkownika: {ex.Message}", "OK");
            }
        }










        public async Task<Firma> ZalogujFirme(string email, string haslo)
        {
            return await _database.Table<Firma>().FirstOrDefaultAsync(u => u.email == email && u.haslo == haslo);
        }
        public event EventHandler<string> PobieranieDanychDoswiadczeniaError;

     


    }
    public class InformacjeOUzytkowniku
    {
        [PrimaryKey,AutoIncrement]
        public int Id {  get; set; }
        public Uzytkownik Uzytkownik { get; set; }
        public List<Wyksztalcenie> Wyksztalcenie { get; set; }
        public List<Umiejetnosc> Umiejetnosci { get; set; }
        public List<Kurs> Kursy { get; set; }
        public List<Jezyk> Jezyki { get; set; }
        public List<Doswiadczenie> Doswiadczenie { get; set; }
    }
}