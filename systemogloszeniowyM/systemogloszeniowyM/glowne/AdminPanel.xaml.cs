using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace systemogloszeniowyM.glowne
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DodajOgloszenie : ContentPage
    {
        private readonly BazaDanych _dataAccess;
        private Sesja _sesja;
        public DodajOgloszenie ()
		{
			InitializeComponent();
            _dataAccess = new BazaDanych(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BazaDanych.db3"));
            Inicjalizacja();
        }
        private async void Inicjalizacja()
        {
            _sesja = await _dataAccess.PobierzSesje();

            if (_sesja != null)
            {
                if (_sesja.TypZalogowanego == "Uzytkownik")
                {

                }
                else if (_sesja.TypZalogowanego == "Firma")
                {
                    
                }
            }
        }

        private async  void Dodaj_Ogloszenie(object sender, EventArgs e)
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
            Ogloszenie noweOgloszenie = new Ogloszenie
            {
                Idfirmy = _sesja.Id,
                Nazwa = NazwaOfertyEntry.Text,
                PoziomStanowiska = PoziomStanowiskaEntry.Text,
                RodzajUmowy = RodzajUmowyEntry.Text,
                WymiarEtatu = WymiarEtatuEntry.Text,
                RodzajPracy = RodzajPracyEntry.Text,
                Wynagrodzenie = WynagrodzenieEntry.Text,
                DniPracy = DniPracyEntry.Text, 
                Godzinypracy = GodzinyPracyEntry.Text,
                DataWaznosci = DataWaznosciEntry.Text,
                Kategoria = KategoriaEntry.Text,
                ZakresObowiazkow = ZakresObowiazkowEntry.Text,
                OferowaneBenefity = OferowaneBenefityEntry.Text,
                Wymagania = WymaganiaEntry.Text,
                Informacje = InformacjeEntry.Text
            };
            _sesja =  _dataAccess.PobierzSesje().Result;

            _dataAccess.DodajOgloszenie(_sesja, noweOgloszenie);

            PrzyciskDodaj.IsVisible = true;
            FormularzDodawanieOgloszenia.IsVisible = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd: {ex.Message}", "OK");
            }
        }


        private void AnulujDodawanieOgloszenia(object sender, EventArgs e)
        {
            FormularzDodawanieOgloszenia.IsVisible = false;
            PrzyciskDodaj.IsVisible = true;
        }

        private void OdpalFormularzDoDodawaniaOgloszenia(object sender, EventArgs e)
        {
            FormularzDodawanieOgloszenia.IsVisible = true;
            PrzyciskDodaj.IsVisible = false;

        }
    }
}