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
                    PrzyciskDodaj.IsVisible = false;

                }
                else if (_sesja.TypZalogowanego == "Firma")
                {
                    List<WyswietlanieOgloszeniaIFirmy> list = new List<WyswietlanieOgloszeniaIFirmy>();
                    foreach (var item in App.DataAccess.PobierzOgloszneia())
                    {
                        if (item.Idfirmy == _sesja.idUzytkownika)
                        {
                            Firma firm = App.DataAccess.PobierzFirme(_sesja.idUzytkownika);
                            list.Add(new WyswietlanieOgloszeniaIFirmy(item, firm));
                        }
                    }
                    ListaOgloszen.ItemsSource = list;
                }
            }
        }

        private void Dodaj_Ogloszenie(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NazwaOfertyEntry.Text) ||
                    string.IsNullOrEmpty(PoziomStanowiskaEntry.Text) ||
                    string.IsNullOrEmpty(RodzajUmowyEntry.Text) ||
                    string.IsNullOrEmpty(WymiarEtatuEntry.Text) ||
                    string.IsNullOrEmpty(RodzajPracyEntry.Text) ||
                    string.IsNullOrEmpty(WynagrodzenieEntry.Text) ||
                    string.IsNullOrEmpty(DniPracyEntry.Text) ||
                    string.IsNullOrEmpty(GodzinyPracyEntry.Text) ||
                    string.IsNullOrEmpty(DataWaznosciEntry.Text) ||
                    string.IsNullOrEmpty(KategoriaEntry.Text) ||
                    string.IsNullOrEmpty(ZakresObowiazkowEntry.Text) ||
                    string.IsNullOrEmpty(OferowaneBenefityEntry.Text) ||
                    string.IsNullOrEmpty(WymaganiaEntry.Text) ||
                    string.IsNullOrEmpty(InformacjeEntry.Text))
                {
                    DisplayAlert("Błąd", "Wszystkie pola muszą być wypełnione", "OK");
                    return;
                }

                Ogloszenie noweOgloszenie = new Ogloszenie
                {
                    Idfirmy = _sesja.idUzytkownika,
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
                _dataAccess.DodajOgloszenie(_sesja, noweOgloszenie);

                PrzyciskDodaj.IsVisible = true;
                FormularzDodawanieOgloszenia.IsVisible = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd: {ex.Message}", "OK");
            }
        }

        private void Usun_Ogloszenie(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.BindingContext as WyswietlanieOgloszeniaIFirmy;

            if (item != null)
            {
                _dataAccess.UsunOgloszenie(item.Id);
                Inicjalizacja();
            }
        }

        private void Edytuj_Ogloszenie(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.BindingContext as WyswietlanieOgloszeniaIFirmy;
            Navigation.PushAsync(new EdytujOgloszenie(item.ogloszenie));
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

        private void Wyloguj(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}