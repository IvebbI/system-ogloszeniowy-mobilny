using System;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using systemogloszeniowyM.Tabele;
using System.IO;

namespace systemogloszeniowyM.glowne
{
    public partial class SzczegolyOgloszenia : ContentPage
    {
        private readonly BazaDanych _dataAccess;
        private WyswietlanieOgloszeniaIFirmy _wyswietlaneOgloszenie;
        private Sesja _sesja;
        public SzczegolyOgloszenia(WyswietlanieOgloszeniaIFirmy ogloszenie)
        {
            InitializeComponent();
            WypelnijDane(ogloszenie);
            _dataAccess = new BazaDanych(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BazaDanych.db3"));
            Inicjalizacja();
        }

        private void WypelnijDane(WyswietlanieOgloszeniaIFirmy ogloszenie)
        {
            NazwaLabel.Text = ogloszenie.Nazwa;
            PoziomStanowiskaLabel.Text = ogloszenie.PoziomStanowiska;
            RodzajUmowy.Text = ogloszenie.RodzajUmowy;
            WymiarEtatu.Text = ogloszenie.WymiarEtatu;
            RodzajPracyLabel.Text = ogloszenie.RodzajPracy;
            WynagrodzenieLabel.Text = ogloszenie.Wynagrodzenie;
            Dnipracy.Text = ogloszenie.DniPracy;
            GodzinyPracy.Text = ogloszenie.DniPracy;
            KategoriaLabel.Text = ogloszenie.Kategoria;
            ZakresObowiazkow.Text = ogloszenie.ZakresObowiazkow;
            OferowaneBenefity.Text = ogloszenie.OferowaneBenefity;
            NazwaFirmyLabel.Text = ogloszenie.NazwaFirmy;
            AdresLabel.Text = ogloszenie.Adres;
            Wymagania.Text = ogloszenie.Wymagania;
            InformacjeLabel.Text = ogloszenie.Informacje;
            _wyswietlaneOgloszenie = ogloszenie; 
        }
        private async void Inicjalizacja()
        {
            _sesja = await _dataAccess.PobierzSesje();

            if (_sesja != null)
            {
                if (_sesja.TypZalogowanego == "Uzytkownik")
                {
                   ButtonAplikuj.IsVisible = true;
                }
                else if (_sesja.TypZalogowanego == "Firma")
                {

                }
            }
        }
        private async void OnAplikujClicked(object sender, EventArgs e)
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();

                if (_sesja != null)
                {
                    DisplayAlert("test", _sesja.Id.ToString(), "ok");

                    if (_wyswietlaneOgloszenie != null)
                    {
                        DisplayAlert("test1", _wyswietlaneOgloszenie.Id.ToString(), "ok");

                        bool czyAplikowal = await _dataAccess.CzyUzytkownikAplikowal(_sesja.Id, _wyswietlaneOgloszenie.Id);

                        if (czyAplikowal)
                        {
                            DisplayAlert("Informacja", "Już aplikowałeś do tego ogłoszenia.", "OK");
                        }
                        else
                        {
                            Aplikacja nowaAplikacja = new Aplikacja
                            {
                                IdUzytkownika = _sesja.Id,
                                IdOgloszenia = _wyswietlaneOgloszenie.Id,
                                StatusAplikacje = "tak"
                            };

                            await _dataAccess.DodajAplikacje(nowaAplikacja);

                            DisplayAlert("Sukces", "Aplikacja została wysłana.", "OK");
                        }
                    }
                    else
                    {
                        DisplayAlert("Błąd", "_wyswietlaneOgloszenie jest równy null.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", ex.ToString(), "OK");
            }
        }





        private void Anuluj(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
