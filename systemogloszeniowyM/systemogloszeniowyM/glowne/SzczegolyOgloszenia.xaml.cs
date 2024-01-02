using System;
using Xamarin.Forms;

namespace systemogloszeniowyM.glowne
{
    public partial class SzczegolyOgloszenia : ContentPage
    {
        public SzczegolyOgloszenia(WyswietlanieOgloszeniaIFirmy ogloszenie)
        {
            InitializeComponent();
            WypelnijDane(ogloszenie);
        }

        private void WypelnijDane(WyswietlanieOgloszeniaIFirmy ogloszenie)
        {
            NazwaLabel.Text = ogloszenie.Nazwa;
            PoziomStanowiskaLabel.Text = ogloszenie.PoziomStanowiska;
            RodzajPracyLabel.Text = ogloszenie.RodzajPracy;
            WynagrodzenieLabel.Text = ogloszenie.Wynagrodzenie;
            KategoriaLabel.Text = ogloszenie.Kategoria;
            NazwaFirmyLabel.Text = ogloszenie.NazwaFirmy;
            AdresLabel.Text = ogloszenie.Adres;
        }
    }
}
