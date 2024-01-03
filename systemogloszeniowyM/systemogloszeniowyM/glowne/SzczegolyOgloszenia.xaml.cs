using System;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using systemogloszeniowyM.Tabele;
namespace systemogloszeniowyM.glowne
{
    public partial class SzczegolyOgloszenia : ContentPage
    {
        private WyswietlanieOgloszeniaIFirmy _wyswietlaneOgloszenie;
        private readonly BazaDanych _dataAccess;
        private Sesja _sesja;
        public SzczegolyOgloszenia(WyswietlanieOgloszeniaIFirmy ogloszenie)
        {
            InitializeComponent();
            WypelnijDane(ogloszenie);
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
        private void OnAplikujClicked(object sender, EventArgs e)
        {
            try
            {
                _sesja = _dataAccess.PobierzSesje().Result;
                if (_sesja != null)
                {
                    DisplayAlert("test", _sesja.Id.ToString(), "ok");
                    DisplayAlert("test1", _wyswietlaneOgloszenie.Id.ToString(), "ok");
                    Aplikacja nowaAplikacja = new Aplikacja
                    {
                        IdUzytkownika = _sesja.Id,
                        IdOgloszenia = _wyswietlaneOgloszenie.Id,
                        StatusAplikacje = "tak"
                    };

                }
            }
            catch(Exception ex)
            {
                DisplayAlert("blad", ex.ToString(), "ok");
            }
            
          
        }

        private void Anuluj(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
