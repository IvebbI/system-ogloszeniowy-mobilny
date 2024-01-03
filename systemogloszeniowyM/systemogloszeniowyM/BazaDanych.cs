using SQLite;
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
        public Doswiadczenie PobierzDaneDoswiadczenia(Sesja sesja)
        {
            try
            {
                return _database.Table<Doswiadczenie>().Where(item => item.IdUzytkownika == sesja.idUzytkownika).FirstAsync().Result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public Jezyk PobierzDaneJezyku(Sesja sesja)
        {
            try
            {
                return _database.Table<Jezyk>().Where(item => item.IdUzytkownika == sesja.idUzytkownika).FirstAsync().Result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public Umiejetnosc PobierzDaneUmiejetnosci(Sesja sesja)
        {
            try
            {
                return _database.Table<Umiejetnosc>().Where(item => item.IdUzytkownika == sesja.idUzytkownika).FirstAsync().Result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public Wyksztalcenie PobierzDaneWyksztalcenia(Sesja sesja)
        {
            try
            {
                return _database.Table<Wyksztalcenie>().Where(item => item.IdUzytkownika == sesja.idUzytkownika).FirstAsync().Result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public Kurs PobierzDaneKursu(Sesja sesja)
        {
            try
            {
                return _database.Table<Kurs>().Where(item => item.IdUzytkownika == sesja.idUzytkownika).FirstAsync().Result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public Firma PobierzDaneFirmy(Sesja sesja)
        {
            try
            {
                return _database.Table<Firma>().Where(item => item.Id == sesja.idUzytkownika).FirstAsync().Result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task ZapiszDaneFirmy(Firma firma)
        {
            try
            {
                var result = await _database.InsertOrReplaceAsync(firma);
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

        public async Task ZapiszDaneDoswiadczenia(Doswiadczenie doswiadczenie)
        {
            try
            {
                var result = await _database.InsertOrReplaceAsync(doswiadczenie);
                if (result != 0)
                {

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd", "Nie udało się zapisać danych Doswiadczenia. Result = 0", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas zapisywania danych Doswiadczenia: {ex.Message}", "OK");
            }
        }
        public async Task ZapiszDanejezyku(Jezyk jezyk)
        {
            try
            {
                var result = await _database.InsertOrReplaceAsync(jezyk);
                if (result != 0)
                {

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd", "Nie udało się zapisać danych Doswiadczenia. Result = 0", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas zapisywania danych Doswiadczenia: {ex.Message}", "OK");
            }
        }
        public async Task ZapiszDaneUmiejetnosci(Umiejetnosc umiejetnosc)
        {
            try
            {
                var result = await _database.InsertOrReplaceAsync(umiejetnosc);
                if (result != 0)
                {

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd", "Nie udało się zapisać danych Doswiadczenia. Result = 0", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas zapisywania danych Doswiadczenia: {ex.Message}", "OK");
            }
        }
        public async Task ZapiszDaneWyksztalcenia(Wyksztalcenie wyksztalcenie)
        {
            try
            {
                var result = await _database.InsertOrReplaceAsync(wyksztalcenie);
                if (result != 0)
                {

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd", "Nie udało się zapisać danych Doswiadczenia. Result = 0", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas zapisywania danych Doswiadczenia: {ex.Message}", "OK");
            }
        }
        public async Task ZapiszDaneKursu(Kurs kurs)
        {
            try
            {
                var result = await _database.InsertOrReplaceAsync(kurs);
                if (result != 0)
                {

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd", "Nie udało się zapisać danych Doswiadczenia. Result = 0", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas zapisywania danych Doswiadczenia: {ex.Message}", "OK");
            }
        }





        public List<Ogloszenie> PobierzOgloszneia()
        {
            return _database.Table<Ogloszenie>().ToListAsync().Result;
            
        }
        public List<Firma> PobierzFirme()
        {
            return _database.Table<Firma>().ToListAsync().Result;

        }





        public async Task<Firma> ZalogujFirme(string email, string haslo)
        {
            return await _database.Table<Firma>().FirstOrDefaultAsync(u => u.email == email && u.haslo == haslo);
        }
        public event EventHandler<string> PobieranieDanychDoswiadczeniaError;

        public int DodajOgloszenie(Sesja sesja, Ogloszenie ogloszenie)
        {
            ogloszenie.Idfirmy = sesja.idUzytkownika;
            return _database.InsertAsync(ogloszenie).Result;
        }
        public async Task<bool> CzyUzytkownikAplikowal(int idUzytkownika, int idOgloszenia)
        {

            var aplikacja = await _database.Table<Aplikacja>()
                .Where(a => a.IdUzytkownika == idUzytkownika && a.IdOgloszenia == idOgloszenia)
                .FirstOrDefaultAsync();


            return aplikacja != null;
        }
        public async Task DodajAplikacje(Aplikacja aplikacja)
        {
            await _database.InsertAsync(aplikacja);
        }


    }
    public class InformacjeOUzytkowniku
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Uzytkownik Uzytkownik { get; set; }
        public List<Wyksztalcenie> Wyksztalcenie { get; set; }
        public List<Umiejetnosc> Umiejetnosci { get; set; }
        public List<Kurs> Kursy { get; set; }
        public List<Jezyk> Jezyki { get; set; }
        public List<Doswiadczenie> Doswiadczenie { get; set; }
    }
    public class WyswietlanieOgloszeniaIFirmy
    {
        public Ogloszenie ogloszenie { get; set; }
        public Firma firma { get; set; }
        public string Nazwa { get; set; }
        public string PoziomStanowiska { get; set; }
        public string RodzajPracy { get; set; }
        public string Wynagrodzenie { get; set; }
        public string Kategoria { get; set; }
        public string NazwaFirmy { get; set; }
        public string Adres { get; set; }
        public string RodzajUmowy { get; set; }
        public string WymiarEtatu { get; set; }
        public string DniPracy { get; set; }
        public string GodzinyPracy { get; set; }
        public string DataWaznosci { get; set; }
        public string ZakresObowiazkow { get; set; }
        public string Wymagania { get; set; }
        public string Informacje { get; set; }
        public string OferowaneBenefity { get; set; }
        public int Id { get; set; }
        public WyswietlanieOgloszeniaIFirmy(Ogloszenie ogloszenie, Firma firma)
        {
            Id = ogloszenie.Id;
            this.ogloszenie = ogloszenie;
            this.firma = firma;
            Kategoria = ogloszenie.Kategoria;
            Nazwa = ogloszenie.Nazwa;
            PoziomStanowiska = ogloszenie.PoziomStanowiska;
            RodzajPracy = ogloszenie.RodzajPracy;
            Wynagrodzenie = ogloszenie.Wynagrodzenie;
            Kategoria = ogloszenie.Kategoria;
            NazwaFirmy = firma.Nazwa;
            Adres = firma.Adres;
            RodzajUmowy = ogloszenie.RodzajUmowy;
            WymiarEtatu = ogloszenie.WymiarEtatu;
            DniPracy = ogloszenie.DniPracy;
            GodzinyPracy = ogloszenie.Godzinypracy;
            ZakresObowiazkow = ogloszenie.ZakresObowiazkow;
            OferowaneBenefity = ogloszenie.OferowaneBenefity;
            Wymagania = ogloszenie.Wymagania;
            Informacje = ogloszenie.Informacje;

        }
        public WyswietlanieOgloszeniaIFirmy()
        {

        }
    }


}
